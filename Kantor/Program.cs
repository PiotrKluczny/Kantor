using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Kantor
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .Build();

            CreateHostBuilder(args).Build().Run();

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .MinimumLevel.Information()
            //dodalismy do logow ze logi z microsoftu beda dodawane do logow ale od poziomu warning
            //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //dodalismy do logow ze logi z systemu beda dodawane do logow ale od poziomu error
            //.MinimumLevel.Override("System", LogEventLevel.Error)
            .WriteTo.Console()
            .WriteTo.File("log.txt")
            .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
               
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
               
            }
            finally
            {
                Log.CloseAndFlush();
            }



        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }
}
