using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
