using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(test_for_authentication.Startup))]
namespace test_for_authentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
