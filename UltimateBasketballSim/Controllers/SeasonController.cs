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

        [Authorize]
        private SeasonService CreateSeasonService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var seasonService = new SeasonService(userId);
            return seasonService;

        }

        [HttpPost]
        public IHttpActionResult CreateSeason(SeasonCreate season)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSeasonService();

            if (!service.CreateSeason(season))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetAllSeasons()
        {
            SeasonService service = CreateSeasonService();

            var seasons = service.GetAllSeasons();
            return Ok(seasons);
        }

    }
}
