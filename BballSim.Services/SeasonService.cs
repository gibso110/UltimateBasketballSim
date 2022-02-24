using BballSim.Data;
using BballSim.Models.SeasonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Services
{
    public class SeasonService
    {
        private readonly Guid _userId;

        //LeaugeService constructor that the controller will use to create LeagueService with the
        //user's ID and then use that instance to perform CRUD on League models.
        public SeasonService(Guid userId)
        {
            _userId = userId;
        }

        //Create a season in the database
        //take the seasonCreate model properties and assign them to the entity that then gets written to the DB via the ApplicationDBContext.
        public bool CreateSeason(SeasonCreate model)
        {
            Season seasonEntity = new Season()
            {
                SeasonNumber = model.SeasonNumber,
                LeagueId = model.LeagueId

            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Seasons.Add(seasonEntity);
                return (ctx.SaveChanges() == 1);
            }
        }

        //get all seasons
        public IEnumerable<SeasonListItem> GetAllSeasons()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var seasons = ctx.Seasons
                    .Select(s =>
                    new SeasonListItem
                    {
                        SeasonId = s.SeasonId,
                        LeagueId = s.LeagueId,
                        SeasonNumber = s.SeasonNumber
                        
                    });
                return seasons.ToArray();
            }
        }

        //get season by id
        public Season GetSeasonById(int seasonId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Season seasonToReturn = (Season)ctx.Seasons
                    .Where(s => s.SeasonId == seasonId)
                    .Select(s =>
                    new SeasonListItem
                    {
                        SeasonId = s.SeasonId,
                        LeagueId = s.LeagueId,
                        SeasonNumber = s.SeasonNumber
                    });

                return seasonToReturn;
            }
        }

        ////update a season
        //public bool UpdateSeason(int leagueId, LeagueUpdate updatedLeauge)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        League leagueToUpudate = (League)ctx.Leagues
        //            .Single(l => l.LeagueId == leagueId);

        //        leagueToUpudate.IsActive = updatedLeauge.IsActive;

        //        return ctx.SaveChanges() == 1;
        //    }
        //}

        ////delete a season
        //public bool DeleteSEason(int leagueId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        League leagueTodelete = ctx.Leagues.Find(leagueId);
        //        ctx.Leagues.Remove(leagueTodelete);
        //        return (ctx.SaveChanges() == 1);
        //    }
        //}
    }
}
