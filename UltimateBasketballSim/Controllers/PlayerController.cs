using BballSim.Data;
using BballSim.Models.PlayerModels;
using BballSim.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace UltimateBasketballSim.Controllers
{
    [Authorize]
    public class PlayerController : ApiController
    {
        private PlayerServices CreatePlayerServices()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var playerService = new PlayerServices(userId);
            return playerService;
        }

        [HttpPost]
        public IHttpActionResult CreateAPlayer(PlayerCreate player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlayerServices();

            if (!service.CreatePlayer(player))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetAllPlayers()
        {
            PlayerServices playerService = CreatePlayerServices();
            var player = playerService.GetPlayers();
            return Ok(player);
        }

        [HttpGet]
        public IHttpActionResult GetPlayerById(int id)
        {
            PlayerServices playerService = CreatePlayerServices();
            var player = playerService.GetPlayerById(id);
            return Ok(player);
        }

        [HttpPut]
        public IHttpActionResult EditAPlayer(PlayerEdit player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreatePlayerServices();

            if (!service.UpdatePlayer(player))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeletePlayerById(int id)
        {
            var service = CreatePlayerServices();

            if (!service.DeletePlayer(id))
                return InternalServerError();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AssignFreeAgentsToTeam(int teamId)
        {
            var service = CreatePlayerServices();

            if (!service.AssignFreeAgentTeamId(teamId))
                return InternalServerError();

            return Ok();
        }

    }
}
