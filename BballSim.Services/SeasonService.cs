using BballSim.Data;
using BballSim.Models.SeasonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BballSim.Services
{
    public class SeasonService
    {
        private readonly Guid _userId;

        public SeasonService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSeason(SeasonCreate model)
        {
            var entity =
                new Season()
                {
                    SeasonNumber = model.SeasonNumber
                };
            
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Seasons.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SeasonListItem> GetAllSeasons()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Seasons
                        .Select(
                            s =>
                                new SeasonListItem
                                {
                                    SeasonId = s.SeasonId,
                                    LeagueId = s.LeagueId,
                                    SeasonNumber = s.SeasonNumber
                                }
                                );
                return query.ToArray();
            }
        }

        public IEnumerable<SeasonListItem> GetSeasonById(int seasonId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Seasons.Where(s => s.SeasonId == seasonId)
                    .Select(
                        s =>
                            new SeasonListItem
                            {
                                SeasonId = s.SeasonId,
                                LeagueId = s.LeagueId,
                                SeasonNumber = s.SeasonNumber
                            }
                            );
                return query.ToArray();
            }
        }


    }
}
