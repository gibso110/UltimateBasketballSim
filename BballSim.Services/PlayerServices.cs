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
    }
}
