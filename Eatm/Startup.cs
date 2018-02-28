using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Eatm.Startup))]
namespace Eatm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
