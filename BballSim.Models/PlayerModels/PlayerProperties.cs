using static BballSim.Data.Player;

namespace BballSim.Models.PlayerModels
{
    public class PlayerProperties
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; }
        public Position PlayerPosition { get; set; }
        public int Number { get; set; }
        public double Height { get; set; }
        public double PlayerRating { get; set; }
        public int ? TeamId { get; set; }
    }
}
