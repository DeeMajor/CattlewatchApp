using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CattlewatchMVC.Startup))]
namespace CattlewatchMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
