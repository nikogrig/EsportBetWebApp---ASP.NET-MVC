using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UltraBet.Services.BetService.Contracts;
using UltraBet.Services.EventService.Contracts;
using UltraBet.Services.MatchService.Contracts;
using UltraBet.Services.OddService.Contracts;
using UltraBet.Services.SportService.Contracts;
using UltraBet.Web.Models.Home;

namespace UltraBet.Web.Hubs
{
    public class NotificationsHub : Hub
    {

        private readonly ISportService sportService;
        private readonly IEventService eventService;
        private readonly IMatchService matchService;
        private readonly IBetService betService;
        private readonly IOddService oddService;

        public NotificationsHub(ISportService sportService, IEventService eventService, IMatchService matchService, IBetService betService, IOddService oddService)//, IHubContext<NotificationsHub> hubContext)
        {
            this.sportService = sportService;
            this.eventService = eventService;
            this.matchService = matchService;
            this.betService = betService;
            this.oddService = oddService;
        }

        public async Task Send()
        {
            var model = new HomeViewModel
            {
                Events = this.eventService.GetListOfEvents(),
                Matches = this.matchService.GetListOfMatches(),
                Bets = this.betService.GetListOfBets(),
                Odds = this.oddService.GetListOfOdds(),
            };

            string message = JsonConvert.SerializeObject(model, Formatting.Indented);

            await this.Clients.All.InvokeAsync("Send", message);
        }
    }
}
