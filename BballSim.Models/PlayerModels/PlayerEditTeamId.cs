using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Models.PlayerModels
{
    public class PlayerEditTeamId
    {
        [Required]
        public int TeamId { get; set; }
    }
}
