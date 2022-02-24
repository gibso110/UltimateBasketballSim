using System;
using BballSim.Data;
using System.Collections.Generic;
using BballSim.Models.TeamModels;
using System.Linq;

namespace BballSim.Services
{
        // Hope you get well soon Nick!
    public class TeamServices
    {
        private readonly Guid _userId;

        public TeamServices(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateTeam(TeamCreate model)
        {
            var entity =
                new Team()
                {
                    TeamName = model.TeamName
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Teams.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TeamListItem> GetAllTeams()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Teams
                        .Select(
                            t =>
                                new TeamListItem
                                {
                                    TeamId = t.TeamId,
                                    TeamName = t.TeamName,
                                    WLRecord = t.WLRecord
                                }
                                );
                return query.ToArray();
            }
        }

        public TeamDetail GetTeamById(int teamId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Teams.Single(t => t.TeamId == teamId);

                return new TeamDetail
                {
                    TeamId = query.TeamId,
                    TeamName = query.TeamName,
                    WLRecord = query.WLRecord,
                    GamesPlayed = query.GamesPlayed
                };
            }
        }

        public bool UpdateTeam(TeamEdit model, int teamId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Teams
                        .Single(e => e.TeamId == teamId);
                entity.WLRecord = model.WLRecord;
                entity.GamesPlayed = model.GamesPlayed;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTeam(int teamId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Teams
                        .Single(e => e.TeamId == teamId);
                ctx.Teams.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
