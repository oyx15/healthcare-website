using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Test_Website.Startup))]
namespace Test_Website
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
