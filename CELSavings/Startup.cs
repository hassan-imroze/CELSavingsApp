using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CELSavings.Startup))]
namespace CELSavings
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
