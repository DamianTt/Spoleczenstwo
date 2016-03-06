using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpoleczenstwoInformacyjne.Startup))]
namespace SpoleczenstwoInformacyjne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
