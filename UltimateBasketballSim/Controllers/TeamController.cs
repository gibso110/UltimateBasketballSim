using BballSim.Models.TeamModels;
using BballSim.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace UltimateBasketballSim.Controllers
{
    public class TeamController : ApiController
    {
        private TeamServices CreateTeamService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var teamService = new TeamServices(userId);
            return teamService;
        }

        public IHttpActionResult Post(TeamCreate team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTeamService();

            if (!service.CreateTeam(team))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(TeamEdit team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTeamService();

            if (!service.UpdateTeam(team))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int teamId)
        {
            var service = CreateTeamService();

            if (!service.DeleteTeam(teamId))
                return InternalServerError();

            return Ok();
        }
        
        public IHttpActionResult GetTeamByTeamId(int teamId)
        {
            var service = CreateTeamService();
            var team = service.GetTeamById(teamId);
            return Ok(team);
        }

        public IHttpActionResult GetAllTeam()
        {
            TeamServices service = CreateTeamService();
            var team = service.GetAllTeams();
            return Ok(team);
        }
    }
}