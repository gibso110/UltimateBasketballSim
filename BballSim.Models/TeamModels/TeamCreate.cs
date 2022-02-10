using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
