using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Data
{
    public class Game
    {
        //Game entity. Properties for the game object:
        [Key]
        public int GameId { get; set; }

        [ForeignKey("Team1"), Required]
        public int Team1Id { get; set; }
        public virtual Team Team1 { get; set; }

        [ForeignKey("Team2"), Required]
        public int Team2Id { get; set; }
        public virtual Team Team2 { get; set; }

        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public DateTime GameDate { get; set; }
        
        [ForeignKey("Season"), Required]
        public int SeasonId { get; set; }
        public virtual Season Season { get; set; }
    }
}
