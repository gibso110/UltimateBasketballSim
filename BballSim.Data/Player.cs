using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Data
{
    public class Player
    {
        public enum Position { Pg, Sg, Sf, Pf, C }
        [Key]
        public int PlayerId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public Position PlayerPosition { get; set; }

        [Required]
        public int Number { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double PlayerRating { get; set; }
    }
}
