using Hangfire;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;
using UltraBet.Services.BetService.Contracts;
using UltraBet.Services.EventService.Contracts;
using UltraBet.Services.MatchService.Contracts;
using UltraBet.Services.OddService.Contracts;
using UltraBet.Services.SportService.Contracts;
using UltraBet.Services.XmlService.Constracts;
using static UltraBet.Constants.XmlConstants;

namespace UltraBet.Services.XmlService.Implementations
{
    public class XmlService : IXmlService
    {
        private readonly IBetService betService;
        private readonly IEventService eventService;
        private readonly IMatchService matchService;
        private readonly IOddService oddService;
        private readonly ISportService sportService;



        public XmlService(IBetService betService, IEventService eventService, IMatchService matchService, IOddService oddService, ISportService sportService)
        {
            this.betService = betService;
            this.eventService = eventService;
            this.matchService = matchService;
            this.oddService = oddService;
            this.sportService = sportService;
        }

        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public void GetWebClient()
        {
            var client = new WebClient();

            client.Encoding = Encoding.Unicode;

            client.DownloadFile(XML_URL, XML_FILE_NAME);

            client.Dispose();

            string exlString = File.ReadAllText(XML_FILE_NAME);

            var elements = XDocument
                                .Parse(exlString)
                                .Root
                                .Elements();

            DateTime xmlCurrentDateTime = this.CreateCurrentDateTime(XDocument.
                                                    Parse(exlString)
                                                    .Element(XML_ELEMENT_NAME)
                                                    .Attribute(XML_CREATE_DATE_VALUE)
                                                    .Value);


            // If Db Elements are matched with time span
            this.UpdateDbWithCurrentElements(elements, xmlCurrentDateTime);

            //If Matches is no longer available
            this.DeleteOldMatches(xmlCurrentDateTime);

            this.betService.EraseBetWithoutOdds();

            this.matchService.EraseMatchWithoutBets();

            this.eventService.EraceEventsWithoutMatch();
        }

        private void DeleteOldMatches(DateTime xmlCurrentDateTime)
        {
            this.matchService.DeleteOldElements(xmlCurrentDateTime);
        }

        private void UpdateDbWithCurrentElements(IEnumerable<XElement> elements, DateTime xmlCurrentDateTime)
        {
            foreach (XElement sport in elements)
            {
                if (this.IsCorrectXml(sport))
                {
                    this.sportService.AddSportElement(sport);
                }

                foreach (var @event in sport.Elements())
                {
                    foreach (var match in @event.Elements())
                    {
                        if (this.IsCorrectDateTime(DateTime.Parse(match.Attribute(MATCH_START_DATE).Value), xmlCurrentDateTime))
                        {
                            if (this.IsCorrectXml(@event))
                            {
                                this.eventService.AddEventElement(@event, sport.Attribute(SPORT_ID).Value);
                            }

                            if (this.IsCorrectXml(match))
                            {
                                this.matchService.AddMatchElement(match, @event.Attribute(EVENT_ID).Value);
                            }

                            foreach (var bet in match.Elements())
                            {     
                                foreach (var odd in bet.Elements())
                                {
                                    if (this.IsCorrectXml(bet))
                                    {
                                        this.betService.AddBetElement(bet, match.Attribute(MATCH_ID).Value);
                                    }

                                    if (this.IsCorrectXml(odd))
                                    {
                                        this.oddService.AddOrUpdateOddElement(odd, bet.Attribute(BET_ID).Value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool IsCorrectDateTime(DateTime matchDateTimeStart, DateTime xmlSportsDateTime)
        {
            if (xmlSportsDateTime <= matchDateTimeStart && matchDateTimeStart <= xmlSportsDateTime.AddHours(24))
            {
                return true;
            }

            return false;
        }

        private bool IsCorrectXml(XElement element)
        {
            if (element.HasAttributes)
            {
                return true;
            }

            return false;
        }

        private DateTime CreateCurrentDateTime(string xmlAttrCreateDate)
        {
            var dateTime = DateTime.Parse(xmlAttrCreateDate);

            return dateTime;
        }
    }
}
