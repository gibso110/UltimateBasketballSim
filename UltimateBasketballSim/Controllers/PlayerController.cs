using BballSim.Models.PlayerModels;
using BballSim.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UltimateBasketballSim.Controllers
{
    [Authorize]
    public class PlayerController : ApiController
    {
        private PlayerServices CreatePlayerServices() 
        {
            var playerService = new PlayerServices();
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


    }
}
