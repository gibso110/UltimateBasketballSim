using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Models.SeasonModels
{
    public class SeasonListItem
    {
        public int SeasonId { get; set; }
        public int LeagueId { get; set; }
        public int SeasonNumber { get; set; } = 1;
    }
}
