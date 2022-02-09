using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Models.GameModels
{
    public class GameCreate
    {
        
        [ForeignKey("Team"), Required]
        public int Team1Id { get; set; }
        [ForeignKey("Team"), Required]
        public int Team2Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public DateTime GameDate { get; set; }
    }
}
