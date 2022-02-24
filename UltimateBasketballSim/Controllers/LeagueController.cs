using BballSim.Models.LeagueModels;
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
    public class LeagueController : ApiController
    {
        [Authorize]

        private LeagueService CreateLeaugeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var gameService = new LeagueService(userId);
            return gameService;
        }


        //Create A League through the API
        //Should return the league ID instead of nothing.
        [HttpPost]
        public IHttpActionResult CreateALeague()
        {
            LeagueService leagueService = CreateLeaugeService();
            if (leagueService.CreateLeauge())
                return Ok();

            return InternalServerError();
        }

        //Get List Of Leauges
        [HttpGet]
        public IHttpActionResult GetListOfLeagues()
        {
            LeagueService leagueService = CreateLeaugeService();
            var listofLeagues = leagueService.GetAllLeagues();
            if (listofLeagues != null)
                return Ok(listofLeagues);

            return InternalServerError();
        }

        //get league by ID
        [HttpGet]
        public IHttpActionResult GetLeagueByID([FromUri] int leagueId)
        {
            LeagueService leagueService = CreateLeaugeService();
            var leagueToReturn = leagueService.GetLeagueByID(leagueId);
            if (leagueToReturn != null)
                return Ok(leagueToReturn);
            return InternalServerError();

        }

        //update a league
        [HttpPut]
        public IHttpActionResult UpdateALeague([FromBody] LeagueUpdate updatedLeauge, int leaugeId)
        {
            LeagueService leagueService = CreateLeaugeService();
            if (leagueService.UpdateLeague(leaugeId, updatedLeauge))
                return Ok();
            return BadRequest();
        }

        //delete a league
        [HttpDelete]
        public IHttpActionResult DeleteALeague(int leaugeId)
        {
            LeagueService leagueService = CreateLeaugeService();
            if (leagueService.DeleteALeague(leaugeId))
                return Ok();
            return InternalServerError();
        }
    }
}
