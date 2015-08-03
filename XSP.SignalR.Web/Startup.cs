using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XSP.SignalR.Web.Startup))]
namespace XSP.SignalR.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapSignalR();
        }
    }
}
