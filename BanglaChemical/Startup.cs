using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BanglaChemical.Startup))]
namespace BanglaChemical
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
