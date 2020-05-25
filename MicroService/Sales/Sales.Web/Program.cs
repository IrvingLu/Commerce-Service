using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Sales.Web
{
    public class Program
    {
        /// <summary>
        /// ¶©µ¥·þÎñ
        /// </summary>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(
                   options =>
                   {
                       options.AddServerHeader = false;
                       options.Listen(IPAddress.Any, 5005);
                   }).UseStartup<Startup>();
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
