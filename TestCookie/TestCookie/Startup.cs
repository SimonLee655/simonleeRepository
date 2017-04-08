using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestCookie.Startup))]
namespace TestCookie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
