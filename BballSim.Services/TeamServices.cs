using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BballSim.Services
{
    public class TeamServices
    {
        private readonly Guid _userId;

        public TeamServices(Guid userId)
        {
            _userId = userId;
        }
    }
}
