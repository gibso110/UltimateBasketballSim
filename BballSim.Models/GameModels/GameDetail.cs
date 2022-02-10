using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Models.GameModels
{
    public class GameDetail
    { 
            public int GameId { get; set; }
            public int Team1Id { get; set; }
            public int Team2Id { get; set; }
            public int Team1Score { get; set; }
            public int Team2Score { get; set; }
            public DateTime GameDate { get; set; }
    }
}
