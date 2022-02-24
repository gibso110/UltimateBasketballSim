using System;
using System.ComponentModel.DataAnnotations;

namespace BballSim.Models.GameModels
{
    public class GameUpdate
    {
        
        [Required, Range(00, 200, ErrorMessage = "Invalid Score (00-200)")]
        public int Team1Score { get; set; }

        [Required, Range(00, 200, ErrorMessage = "Invalid Score (00-200)")]
        public int Team2Score { get; set; }
    }
}
