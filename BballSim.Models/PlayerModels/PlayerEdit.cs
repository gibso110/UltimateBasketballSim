using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BballSim.Data.Player;

namespace BballSim.Models.PlayerModels
{
    public class PlayerEdit
    {
       
        public int PlayerId { get; set; }
        
        [MinLength(4, ErrorMessage = "Please enter a name with at least 4 characters.")]
        public string FullName { get; set; }
        
        public Position PlayerPosition { get; set; }
        
        [Range(00, 99, ErrorMessage = "Please enter a number between 00 and 99")]
        public int Number { get; set; }
        
        [Range(60, 92, ErrorMessage = "Please enter a height between 60 and 92 inches")]
        public int Height { get; set; }
        
        [Range(10, 99, ErrorMessage = "Please enter a height between 10 and 99 inches")]
        public int PlayerRating { get; set; }
        [Required]
        public int TeamId { get; set; }
    }
}
