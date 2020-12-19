using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CRMSystem.Web.Areas.Identity.IdentityHostingStartup))]
namespace CRMSystem.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}