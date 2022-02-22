using System.ComponentModel.DataAnnotations;

namespace BballSim.Models.TeamModels
{
    public class TeamCreate
    {
        [MinLength(3, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(35, ErrorMessage = "There are too many characters in this field.")]
        public string TeamName { get; set; }
    }
}
