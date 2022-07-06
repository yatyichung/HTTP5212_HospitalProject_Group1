using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HTTP5212_HospitalProject_Team1.Startup))]
namespace HTTP5212_HospitalProject_Team1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
