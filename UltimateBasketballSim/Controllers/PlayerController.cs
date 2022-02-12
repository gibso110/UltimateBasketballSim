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

        public IHttpActionResult Post(PlayerCreate player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlayerServices();

            if (!service.CreatePlayer(player))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Get()
        {
            PlayerServices playerService = CreatePlayerServices();
            var player = playerService.GetPlayers();
            return Ok(player);
        }

        public IHttpActionResult Get(int id)
        {
            PlayerServices playerService = CreatePlayerServices();
            var player = playerService.GetPlayerById(id);
            return Ok(player);
        }

        public IHttpActionResult Put(PlayerEdit player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreatePlayerServices();

            if (!service.UpdatePlayer(player))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreatePlayerServices();

            if (!service.DeletePlayer(id))
                return InternalServerError();

            return Ok();
        }

    }
}
