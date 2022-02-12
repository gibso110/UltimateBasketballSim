using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Team"), Required]
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
