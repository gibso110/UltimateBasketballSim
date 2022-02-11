using BballSim.Data;
using BballSim.Models.LeagueModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Services
{
    public class LeagueService
    {
        private readonly Guid _userId;

        //LeaugeService constructor that the controller will use to create LeagueService with the
        //user's ID and then use that instance to perform CRUD on League models.
        public LeagueService(Guid userId)
        {
            _userId = userId;
        }

        //Create a leauge in the database
        //take the leaugeCreate model properties and assign them to the entity that then gets written to the DB via the ApplicationDBContext.
        public bool CreateLeauge()
        {
            League leaugeEntity = new League()
            {
                IsActive = false
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Leagues.Add(leaugeEntity);
                return (ctx.SaveChanges() == 1);
            }
        }

        //get all leagues
        public IEnumerable<LeagueList> GetAllLeagues()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var leagues = ctx.Leagues
                    .Select(l =>
                    new LeagueList
                    {
                        LeagueId = l.LeagueId,
                        IsActive = l.IsActive
                    });
                return leagues.ToArray();
            }
        }

        //get leauge by id
        public League GetLeagueByID(int leagueId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                League leagueToReturn = (League)ctx.Leagues
                    .Where(l => l.LeagueId == leagueId)
                    .Select(l =>
                    new LeagueList
                    {
                        LeagueId = l.LeagueId,
                        IsActive = l.IsActive
                    });

                return leagueToReturn;
            }
        }

        //update a league
        public bool UpdateLeague(int leagueId, LeagueUpdate updatedLeauge)
        {
            using (var ctx = new ApplicationDbContext())
            {
                League leagueToUpudate = (League)ctx.Leagues
                    .Single(l => l.LeagueId == leagueId);

                leagueToUpudate.IsActive = updatedLeauge.IsActive;

                return ctx.SaveChanges() == 1;
            }
        }

        //delete a league
        public bool DeleteALeague(int leagueId)
        {
            using (var ctx = new ApplicationDbContext())
            {
               League leagueTodelete = ctx.Leagues.Find(leagueId);
                ctx.Leagues.Remove(leagueTodelete);
                return (ctx.SaveChanges() == 1);
            }
        }
    }
}
