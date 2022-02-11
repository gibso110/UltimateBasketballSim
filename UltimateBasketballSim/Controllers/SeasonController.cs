using BballSim.Models.SeasonModels;
using BballSim.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UltimateBasketballSim.Controllers
{
    public class SeasonController : ApiController
    {
        private SeasonService CreateSeasonServices()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var seasonService = new SeasonService(userId);
            return seasonService;
        }


        public IHttpActionResult Post(SeasonCreate season)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSeasonServices();

            if (!service.CreateSeason(season))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Get()
        {
            SeasonService seasonService = CreateSeasonServices();
            var season = seasonService.GetAllSeasons();
            return Ok(season);
        }
    }
}
