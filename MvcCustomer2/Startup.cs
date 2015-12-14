using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcCustomer2.Startup))]
namespace MvcCustomer2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
