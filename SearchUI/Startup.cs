using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SearchUI.Startup))]
namespace SearchUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
