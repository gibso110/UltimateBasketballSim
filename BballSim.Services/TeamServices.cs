using System;
using System.Collections.Generic;
using System.Linq;

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
