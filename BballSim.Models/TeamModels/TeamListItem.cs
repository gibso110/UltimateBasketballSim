using System.ComponentModel.DataAnnotations;

namespace BballSim.Models.TeamModels
{
    public class TeamListItem
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        [Display(Name = "Win/Loss Record")]
        public int WLRecord { get; set; }
        [Display(Name = "Games Played")]
        public int GamesPlayed { get; set; }

    }
}
