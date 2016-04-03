using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SI.Startup))]
namespace SI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
