using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(timney_photos.Startup))]
namespace timney_photos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
