using System;
using System.Threading;
using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace api.src
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Thread.Sleep(100);
          
            SeedDb(host);

            host.Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    
                });
                
        private static void SeedDb(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                     var context = services.GetRequiredService<WeggerContext>();
                     DbInitializer.Init(context, services);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
            }
        }
    }
}
