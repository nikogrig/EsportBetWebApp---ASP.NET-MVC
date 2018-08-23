using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UltraBet.Services.BetService.Models;
using UltraBet.Services.EventService.Models;
using UltraBet.Services.MatchService.Models;
using UltraBet.Services.OddService.Models;
using UltraBet.Services.SportService.Models;

namespace UltraBet.Web.Models.Home
{
    public class HomeViewModel
    {
        //[JsonProperty("sports")]
        //public IEnumerable<SportServiceModel> Sports { get; set; }

        //[JsonProperty("events")]
        public IEnumerable<EventsServiceModel> Events { get; set; }

        //[Jso("matches")]
        public IEnumerable<MatchesServiceModel> Matches { get; set; }

        //[JsonProperty("bets")]
        public IEnumerable<BetsServiceModel> Bets { get; set; }

        //[JsonProperty("odds")]
        public IEnumerable<OddsServiceModel> Odds { get; set; }
    }
}
