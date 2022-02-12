using System.ComponentModel.DataAnnotations;

namespace BballSim.Models.TeamModels
{
    public class TeamCreate
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int PlayerId { get; set; }
        [Required]
        public string TeamName { get; set; }
    }
}
