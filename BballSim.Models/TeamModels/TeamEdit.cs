using System.ComponentModel.DataAnnotations;

namespace BballSim.Models.TeamModels
{
    public class TeamEdit
    {
        [Display(Name = "Win/Loss Record")]
        public int WLRecord { get; set; }
        [Display(Name = "Games Played")]
        public int GamesPlayed { get; set; }
    }
}
