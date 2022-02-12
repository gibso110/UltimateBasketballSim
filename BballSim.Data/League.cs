using System.ComponentModel.DataAnnotations;

namespace BballSim.Data
{
    public class League
    {
        [Key]
        public int LeagueId { get; set; }

        public bool IsActive { get; set; }
    }
}
