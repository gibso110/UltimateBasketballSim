using BballSim.Data;
using BballSim.Models.PlayerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Services
{
    public class PlayerServices
    {

        
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
                                PlayerRating = e.PlayerRating

                            }
                            );

                        return query.ToArray();
                    
                    
                    
            }
        }

        public IEnumerable<PlayerProperties> GetPlayerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Players 
                        .Where(e => e.PlayerId == id)
                        .Select(
                            e =>
                            new PlayerProperties
                            {
                                PlayerId = e.PlayerId,
                                FullName = e.FullName,
                                PlayerPosition = e.PlayerPosition,
                                Number = e.Number,
                                Height = e.Height,
                                PlayerRating = e.PlayerRating
                            }
                        );
                return query.ToArray();
            }
        }
    }
}
