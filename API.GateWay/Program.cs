using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GateWay.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, builder) =>
            {
                builder
                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("Ocelot.json");
            })
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           });
    }
}
