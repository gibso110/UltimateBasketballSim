using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Models.TeamModels
{
    public class TeamDetail
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
       // public List<Player> Players { get; set; }
        public decimal WLRecord { get; set; }
        public int GamesPlayed { get; set; }

    }
}
