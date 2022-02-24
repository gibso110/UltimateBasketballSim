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
                GameDate = DateTime.Now,
                SeasonId = model.SeasonId
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
                var gameToReturn = ctx.Games
                    .Single(g => g.GameId == gameId);

                return new GameDetail
                {
                    GameId = gameToReturn.GameId,
                    Team1Id = gameToReturn.Team1Id,
                    Team2Id = gameToReturn.Team2Id,
                    Team1Score = gameToReturn.Team1Score,
                    Team2Score = gameToReturn.Team2Score,
                    GameDate = gameToReturn.GameDate
                };
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
                        GameId = g.GameId,
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

        //update a game by GameId
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
      
        //play a game, pass int two team IDs and a game ID
        public bool PlayAGame(int team1Id, int team2Id, int gameId)
        {
            PlayerServices playerService = new PlayerServices(_userId);
            List<PlayerProperties> team1Players = (List<PlayerProperties>)playerService.GetPlayersByTeamId(team1Id);
            List<PlayerProperties> team2Players = (List<PlayerProperties>)playerService.GetPlayersByTeamId(team2Id);

            int team1Score = 0;
            int team2Score = 0;

            var rand = new Random();

            // Calculate the average player rating of each team and assign it to local variable.
            int team1AvgPlayerRating = (int)(from p in team1Players select p.PlayerRating).Sum()/5;
            int team2AvgPlayerRating = (int)(from p in team2Players select p.PlayerRating).Sum()/5;

            //Determin number of chances to score points for each team:
            int team1NumRolls = team1AvgPlayerRating / 10;
            int team2NumRolls = team2AvgPlayerRating / 10;

            // Add to the scores of each team based on the number of "rolls" they get calculated using the team's average player rating.
            for (int i = 0; i < team1NumRolls; i++)
            {
                team1Score += (int)(rand.NextDouble() * 26);
            }

            for (int i = 0; i < team2NumRolls; i++)
            {           
                team2Score += (int)(rand.NextDouble() * 26);
            }

            while (team1Score == team2Score)
            {
                team1Score += (int)(rand.NextDouble() * 26);
                team2Score += (int)(rand.NextDouble() * 26);
            }

            //call to update the teams using method in this class. Should have exception handling
             UpdateTeamsAfterGame(team1Id, team2Id, team1Score, team2Score);

            //Update the game with the scores of the teams.
            GameService gameService = new GameService(_userId);
            GameUpdate gameResults = new GameUpdate()
            {
                Team1Score = team1Score,
                Team2Score = team2Score
            };
            return gameService.UpdateGame(gameId, gameResults);
        }

        //update teams after a game (WLRecord and games played)
        public void UpdateTeamsAfterGame(int team1Id, int team2Id, int team1Score, int team2Score)
        {
            // Updating teams based on game results
            TeamServices teamService = new TeamServices(_userId);
            TeamDetail team1 = teamService.GetTeamById(team1Id);
            TeamDetail team2 = teamService.GetTeamById(team2Id);
            int team1WLRecord = team1.WLRecord;
            int team2WLRecord = team2.WLRecord;
            int team1GamesPlayed = team1.GamesPlayed;
            int team2GamesPlayed = team2.GamesPlayed;

            // Determining which team wins then adding 1 to their WLRecord, add 1 to both team's GamesPlayed
            if (team1Score > team2Score)
            {
                TeamEdit winner = new TeamEdit();
                winner.WLRecord = team1WLRecord + 1;
                winner.GamesPlayed = team1GamesPlayed + 1; 
                teamService.UpdateTeam(winner, team1Id);

                TeamEdit loser = new TeamEdit();
                loser.GamesPlayed = team2GamesPlayed + 1;
                teamService.UpdateTeam(loser, team2Id);
            }
            else if (team2Score > team1Score)
            {
                TeamEdit winner = new TeamEdit();
                winner.WLRecord = team2WLRecord + 1;
                winner.GamesPlayed = team2GamesPlayed + 1;
                teamService.UpdateTeam(winner, team2Id);

                TeamEdit loser = new TeamEdit();
                loser.GamesPlayed = team1GamesPlayed;
                teamService.UpdateTeam(loser, team1Id);
            }
        }
    }
}
