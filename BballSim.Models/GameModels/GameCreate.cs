using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BballSim.Models.GameModels
{
    public class GameCreate
    {

        [Range(1, 100, ErrorMessage = "Invalid Team ID")]
        public int Team1Id { get; set; }

        [Range(1, 100, ErrorMessage = "Invalid Team ID")]

        public int Team2Id { get; set; }

        public int Team1Score { get; set; } = 0;
        public int Team2Score { get; set; } = 0;
        public DateTime GameDate { get; set; }

        [Required]
        public int SeasonId { get; set; }
    }
}
