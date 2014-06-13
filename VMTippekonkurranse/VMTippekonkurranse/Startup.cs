using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VMTippekonkurranse.Startup))]
namespace VMTippekonkurranse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
