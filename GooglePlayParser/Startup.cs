using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GooglePlayParser.Startup))]
namespace GooglePlayParser
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
