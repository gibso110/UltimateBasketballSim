using System;
using System.ComponentModel.DataAnnotations;

namespace BballSim.Models.GameModels
{
    public class GameDetail
    {
        [Display(Name = "Game Id")]
        public int GameId { get; set; }
        [Display(Name = "Team 1 Id")]
        public int Team1Id { get; set; }
        [Display(Name = "Team 2 Id")]
        public int Team2Id { get; set; }
        [Display(Name = "Team 1 Score")]
        public int Team1Score { get; set; }
        [Display(Name = "Team 2 Score")]
        public int Team2Score { get; set; }
        [Display(Name = "Game Date")]
        public DateTime GameDate { get; set; }
    }
}
