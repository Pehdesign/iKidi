using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iKidi.Startup))]
namespace iKidi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
