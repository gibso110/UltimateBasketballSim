using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Models.GameModels
{
    public class GameList
    {
        public int GameId { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public DateTime GameDate { get; set; }
    }
}
