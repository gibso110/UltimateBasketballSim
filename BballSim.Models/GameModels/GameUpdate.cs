using System;

namespace BballSim.Models.GameModels
{
    public class GameUpdate
    {
        public int GameId { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public DateTime GameDate { get; set; }
    }
}
