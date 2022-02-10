using BballSim.Models.GameModels;
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
    [Authorize]
    public class GameController : ApiController
    {
        private GameService CreateGameService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var gameService = new GameService(userId);
            return gameService;
        }

        [HttpPost]
        public IHttpActionResult CreateGame(GameCreate gameToCreate)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

                var gameService = CreateGameService();

                if (gameService.CreateGame(gameToCreate) == true)
                    return Ok();
                return InternalServerError();
        }

        [HttpGet]
        public IHttpActionResult GetAllGames()
        {
            var gameService = CreateGameService();

            List <GameList> listToreturn = (List<GameList>)gameService.GetGameList();

            if (listToreturn != null)
                return Ok(listToreturn);

            return InternalServerError();

        }

        //get game by ID

        //update a game

        //play a game

        //delete a game
    }
}
