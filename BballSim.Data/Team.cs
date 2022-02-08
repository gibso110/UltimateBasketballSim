using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Data
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        public string TeamName { get; set; }

        [Required]
        public decimal WLRecord { get; set; }

        [Required]
        public int GamesPlayed { get; set; }

        [ForeignKey("Player"), Required]
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        [ForeignKey("League"), Required]
        public int LeagueId { get; set; }
        public League League { get; set; }
    }
}
