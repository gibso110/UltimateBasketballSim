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
                Team1 = model.Team1,
                Team1Score = model.Team1Score,
                Team2 = model.Team2Score,
                Team2Score = model.Team2Score,
                GameDate = DateTime.Now
            };

            //using (var ctx = new ApplicationDbContext())
            //{
            //    ctx.Games.Add(gameEntity);
            //    return (ctx.SaveChanges() == 1);
            //}
            return true;
        }
    }
}
