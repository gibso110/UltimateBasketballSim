using BballSim.Data;
using BballSim.Models.GameModels;
using BballSim.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace UltimateBasketballSim.Controllers
{
    [Authorize]
   // [RoutePrefix("api/Game")]
    public class GameController : ApiController
    {
        private GameService CreateGameService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var gameService = new GameService(userId);
            return gameService;
        }

        //create a new game
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

        //  get a list of all games
        [HttpGet]
        public IHttpActionResult GetAllGames()
        {
            var gameService = CreateGameService();

            var listToreturn = gameService.GetGameList();

            if (listToreturn != null)
                return Ok(listToreturn);

            return InternalServerError();
        }

        //delete a game
        [HttpDelete]
        public IHttpActionResult DeleteAGame([FromBody] int gameId)
        {
            GameService gameService = CreateGameService();

            if (gameService.DeleteGame(gameId))
                return Ok();

            return InternalServerError();
        }

        //get game by ID
        [HttpGet]
        public IHttpActionResult GetGameById([FromUri] int gameId)
        {
            var gameService = CreateGameService();

            GameDetail gameToReturn = gameService.GetGameDetail(gameId);
            if (gameToReturn != null)
                return Ok(gameToReturn);

            return InternalServerError();
        }

        //play a game
        [HttpPost]
        public IHttpActionResult PlayAGame(int team1Id, int team2Id, int gameId)
        {
            var gameService = CreateGameService();

            if (gameService.PlayAGame(team1Id, team2Id, gameId))
                return Ok();

            return InternalServerError();
        }
    }
}
