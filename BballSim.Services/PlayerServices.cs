using BballSim.Data;
using BballSim.Models.PlayerModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BballSim.Services
{
    public class PlayerServices
    {
        private readonly Guid _userId;

        public PlayerServices(Guid id)
        {
            _userId = id;
        }

        public bool CreatePlayer(PlayerCreate model)
        {
            var entity =
                new Player()
                {

                    FullName = model.FullName,
                    PlayerPosition = model.PlayerPosition,
                    Number = model.Number,
                    Height = model.Height,
                    PlayerRating = model.PlayerRating
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Players.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PlayerProperties> GetPlayers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                ctx
                    .Players
                    .Select(
                        e =>
                            new PlayerProperties
                            {
                                PlayerId = e.PlayerId,
                                FullName = e.FullName,
                                PlayerPosition = e.PlayerPosition,
                                Number = e.Number,
                                Height = e.Height,
                                PlayerRating = e.PlayerRating,
                                TeamId = e.TeamId
                            }
                            );

                return query.ToArray();
            }
        }

        public IEnumerable<PlayerProperties> GetFreeAgents()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                ctx
                    .Players
                    .Where(e => e.TeamId == null)
                    .Select(
                        e =>
                            new PlayerProperties
                            {
                                PlayerId = e.PlayerId,
                                FullName = e.FullName,
                                PlayerPosition = e.PlayerPosition,
                                Number = e.Number,
                                Height = e.Height,
                                PlayerRating = e.PlayerRating,
                                TeamId = e.TeamId
                            }
                            );
                return query.ToList();
            }
        }

        public PlayerProperties GetPlayerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == id);

                return new PlayerProperties
                {
                    PlayerId = query.PlayerId,
                    FullName = query.FullName,
                    PlayerPosition = query.PlayerPosition,
                    Number = query.Number,
                    Height = query.Height,
                    PlayerRating = query.PlayerRating,
                    TeamId = query.TeamId
                };
            }
        }

        public IEnumerable<PlayerProperties> GetPlayersByTeamId(int teamId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Players
                        .Where(e => e.TeamId == teamId)
                        .Select(
                            e =>
                            new PlayerProperties
                            {
                                PlayerId = e.PlayerId,
                                FullName = e.FullName,
                                PlayerPosition = e.PlayerPosition,
                                Number = e.Number,
                                Height = e.Height,
                                PlayerRating = e.PlayerRating,
                                TeamId = e.TeamId
                            }
                        );
                return query.ToList();
            }
        }

        public bool UpdatePlayer(PlayerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == model.PlayerId);

                entity.FullName = model.FullName;
                entity.PlayerPosition = model.PlayerPosition;
                entity.Number = model.Number;
                entity.Height = model.Height;
                entity.PlayerRating = model.PlayerRating;
                entity.TeamId = model.TeamId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateTeamIdForPlayer(int playerId, PlayerEditTeamId model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == playerId);

                entity.TeamId = model.TeamId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePlayer(int playerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == playerId);

                ctx.Players.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool AssignFreeAgentTeamId(int teamId)
        {
            // make sure the team doesn't currently have any players assigned:
            //select Count of players with this teamId, if greater than zero, return false.
            int numberOfPlayersOnTeam;
            using (var ctx = new ApplicationDbContext())
            {
                numberOfPlayersOnTeam = ctx.Players.Where(n => n.TeamId == teamId).Count();
            }
            if (numberOfPlayersOnTeam > 0)
                return false;

            //Get list of all players not yet assigned a TeamId
            List<PlayerProperties> listOfFreeAgents = (List<PlayerProperties>)GetFreeAgents();

            // Make sure there are at least 5 Free Agents
            if (listOfFreeAgents.Count() < 5)
                return false;

            //add add'l checks for one player of each position

            // For each position (index in the Position enum)
            //Find the first player of that position and assign them to the team passed in.
            for (int i = 0; i <= 4; i++)
            {
                //find a player with position == i and assign them to the team passed into the method.
                int existingPlayerId = listOfFreeAgents.Find(p => ((int)p.PlayerPosition) == i).PlayerId;
                PlayerEditTeamId playerToUpdate = new PlayerEditTeamId()
                {
                    TeamId = teamId
                };
                //listOfFreeAgents.Remove();
                UpdateTeamIdForPlayer(existingPlayerId, playerToUpdate);
            }

            //check to see if there are 5 less players without team IDs on the free agent list
            if (GetFreeAgents().Count() == listOfFreeAgents.Count() - 5)
                return true;
            return false;
        }
    }
}
