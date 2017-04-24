using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(odonto.Startup))]
namespace odonto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
