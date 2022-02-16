using System.ComponentModel.DataAnnotations;

namespace BballSim.Data
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        public string TeamName { get; set; }

        [Required]
        public int WLRecord { get; set; } = 0;

        [Required]
        public int GamesPlayed { get; set; } = 0;

        //  [ForeignKey("League"), Required]
        // [ForeignKey("League"), Required]
        //  public int LeagueId { get; set; }
        ////  public virtual League League { get; set; }
    }
}
