using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Silkways.Startup))]
namespace Silkways
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
