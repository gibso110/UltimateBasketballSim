using BballSim.Data;
using BballSim.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Services
{
    public class GameService
    {
        private readonly Guid _userId;

        //GameService constructor that the controller will use to create GameService with the
        //user's ID and then use that instance to perform CRUD on Game models.

        public GameService(Guid userId)
        {
            _userId = userId;
        }

        //Create a game in the database
        //take the GameCreate model properties and assign them to the entity that then gets written to the DB via the ApplicationDBContext.
        public bool CreateGame(GameCreate model)
        {
            Game gameEntity = new Game()
            {
                Team1Id = model.Team1Id,
                Team2Id = model.Team2Id,
                GameDate = DateTime.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Games.Add(gameEntity);
                return (ctx.SaveChanges() == 1);
            }
        }

        //get the game details by game ID
        //Can this be refactored to return a 'GameDetail'?
        public IEnumerable<GameDetail> GetGameDetail(int gameId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var gameToReturn = ctx.Games
                    .Where(g => g.GameId == gameId)
                    .Select(
                    g =>
                    new GameDetail
                    {
                        GameId = g.GameId,
                        Team1Id = g.Team1Id,
                        Team2Id = g.Team2Id,
                        Team1Score = g.Team1Score,
                        Team2Score = g.Team2Score,
                        GameDate = g.GameDate
                    }
                    );
                return gameToReturn.ToArray();
            }
        }

        // get game list, returns all games.

        public IEnumerable<GameList> GetGameList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var listOfGames = ctx.Games
                    .Select(g =>
                    new GameList
                    {
                        Team1Id = g.Team1Id,
                        Team2Id = g.Team2Id,
                        GameDate = g.GameDate
                    });

                return listOfGames.ToArray();
            }
        }

        //Delete a game
        public bool DeleteGame(int gameId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var gameToDelete = ctx.Games
                    .Single(g => g.GameId == gameId);

                ctx.Games.Remove(gameToDelete);
                return ctx.SaveChanges() == 1;
            }
        }

        //update a game

        public bool UpdateGame(int gameId, GameUpdate updatedGame)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Game gameToUpdate = ctx.Games
                    .Single(g => g.GameId == gameId);

                gameToUpdate.Team1Score = updatedGame.Team1Score;
                gameToUpdate.Team2Score = updatedGame.Team2Score;

                return ctx.SaveChanges() == 1;
            }
        }

        //play a game
    }
}
