using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Data
{
   public class League
    {
        [Key]
        public int LeagueId { get; set; }
       
        public bool IsActive { get; set; }
    }
}
