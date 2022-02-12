using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UltimateBasketballSim.Startup))]

namespace UltimateBasketballSim
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
