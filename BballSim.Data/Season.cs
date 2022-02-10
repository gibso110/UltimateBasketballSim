using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Data
{
    public class Season
    {
        [Key]
        public int SeasonId { get; set; }

        [ForeignKey("League"), Required]
        public int LeagueId { get; set; }
        public  virtual League League { get; set; }

        [Required]
        public int SeasonNumber { get; set; } = 1;
    }
}
