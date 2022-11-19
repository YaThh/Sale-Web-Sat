using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLBHVoGiaThuan.Startup))]
namespace QLBHVoGiaThuan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
