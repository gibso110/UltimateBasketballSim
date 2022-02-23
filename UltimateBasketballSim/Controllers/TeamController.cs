using BballSim.Models.TeamModels;
using BballSim.Services;
using Microsoft.AspNet.Identity;
using System;
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
        [HttpPost]
        public IHttpActionResult CreateATeam(TeamCreate team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTeamService();

            if (!service.CreateTeam(team))
                return InternalServerError();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateTeamById(TeamEdit team, int teamId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTeamService();

            if (!service.UpdateTeam(team, teamId))
                return InternalServerError();

            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult DeleteTeamById(int teamId)
        {
            var service = CreateTeamService();

            if (!service.DeleteTeam(teamId))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetTeamByTeamId(int teamId)
        {
            var service = CreateTeamService();
            var team = service.GetTeamById(teamId);
            return Ok(team);
        }

        [HttpGet]
        public IHttpActionResult GetAllTeams()
        {
            TeamServices service = CreateTeamService();
            var team = service.GetAllTeams();
            return Ok(team);
        }
    }
}