using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeBudget2.Startup))]
namespace HomeBudget2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
