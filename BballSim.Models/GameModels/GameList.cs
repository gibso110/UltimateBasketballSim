using System;

namespace BballSim.Models.GameModels
{
    public class GameList
    {
        public int GameId { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public DateTime GameDate { get; set; }
    }
}
