using BballSim.Data;
using BballSim.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public GameDetail GetGameDetail(int gameId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                GameDetail gameToReturn = (GameDetail)ctx.Games
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
                return gameToReturn;
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

        //Delete a game by ID
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
        public bool UpdateGame(GameUpdate updatedGame)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Game gameToUpdate = ctx.Games
                    .Single(g => g.GameId == updatedGame.GameId);

                gameToUpdate.Team1Score = updatedGame.Team1Score;
                gameToUpdate.Team2Score = updatedGame.Team2Score;

                return ctx.SaveChanges() == 1;
            }
        }

        //play a game

        public bool PlayAGame(Team team1, Team team2, Game game)
        {
            PlayerServices playerService = new PlayerServices(_userId);

            List<Player> team1Players = (List<Player>) playerService.GetPlayersByTeamId(team1.TeamId);

            List<Player> team2Players = (List<Player>)playerService.GetPlayersByTeamId(team2.TeamId);

            int team1Score = 0;
            int team2Score = 0;

            int team1AvgPlayerRating = (int)(from p in team1Players select p.PlayerRating).Sum()/5;
            int team2AvgPlayerRating = (int)(from p in team2Players select p.PlayerRating).Sum()/5;

            int team1NumRolls = team1AvgPlayerRating / 10;
            int team2NumRolls = team2AvgPlayerRating / 10;

            for (int i = 0; i < team1NumRolls; i++)
            {
                var rand = new Random();

                team1Score += (int)rand.NextDouble() * 13;
            }

            for (int i = 0; i < team2NumRolls; i++)
            {
                var rand = new Random();

                team2Score += (int)rand.NextDouble() * 13;
            }

            GameService gameService = new GameService(_userId);

            GameUpdate gameResults = new GameUpdate()
            {
                Team1Score = team1Score,
                Team2Score = team2Score
            };

           return gameService.UpdateGame(gameResults);

        }
    }
}
