using BballSim.Data;
using BballSim.Models.GameModels;
using BballSim.Models.PlayerModels;
using BballSim.Models.TeamModels;
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

        public bool PlayAGame(int team1Id, int team2Id, int gameId)
        {
            PlayerServices playerService = new PlayerServices(_userId);

            List<PlayerProperties> team1Players = (List<PlayerProperties>)playerService.GetPlayersByTeamId(team1Id);

            List<PlayerProperties> team2Players = (List<PlayerProperties>)playerService.GetPlayersByTeamId(team2Id);

            int team1Score = 0;
            int team2Score = 0;

            var rand = new Random();

            int team1AvgPlayerRating = (int)(from p in team1Players select p.PlayerRating).Sum()/5;
            int team2AvgPlayerRating = (int)(from p in team2Players select p.PlayerRating).Sum()/5;

            int team1NumRolls = team1AvgPlayerRating / 10;
            int team2NumRolls = team2AvgPlayerRating / 10;

            for (int i = 0; i < team1NumRolls; i++)
            {
                team1Score += (int)(rand.NextDouble() * 13);
            }

            for (int i = 0; i < team2NumRolls; i++)
            {           
                team2Score += (int)(rand.NextDouble() * 13);
            }

            while (team1Score == team2Score)
            {
                team1Score += (int)(rand.NextDouble() * 13);
                team2Score += (int)(rand.NextDouble() * 13);
            }

            // Updating teams based on game results
            TeamServices teamService = new TeamServices(_userId);
            TeamDetail team1 = teamService.GetTeamById(team1Id);
            TeamDetail team2 = teamService.GetTeamById(team2Id);

            // Determining which team wins then adding 1 to their WLRecord
            if (team1Score > team2Score)
            {
                int currentWLRecord = team1.WLRecord;
                TeamEdit winner = new TeamEdit();
                winner.WLRecord = currentWLRecord + 1;
                winner.GamesPlayed++;
                teamService.UpdateTeam(winner, team1Id);

                TeamEdit loser = new TeamEdit();
                loser.GamesPlayed++;
                teamService.UpdateTeam(loser, team2Id);
            }
            else if (team2Score > team1Score)
            {
               
                int currentWLRecord = team2.WLRecord;
                TeamEdit winner = new TeamEdit();
                winner.WLRecord = currentWLRecord + 1;
                winner.GamesPlayed++;
                teamService.UpdateTeam(winner, team2Id);

                TeamEdit loser = new TeamEdit();
                loser.GamesPlayed++;
                teamService.UpdateTeam(loser, team1Id);
            }

            GameService gameService = new GameService(_userId);

            GameUpdate gameResults = new GameUpdate()
            {
                Team1Score = team1Score,
                Team2Score = team2Score
            };

            return gameService.UpdateGame(gameId, gameResults);
        }
    }
}
