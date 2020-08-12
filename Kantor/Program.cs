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
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //dodalismy do logow ze logi z systemu beda dodawane do logow ale od poziomu error
            .MinimumLevel.Override("System", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
            .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
            .WriteTo.File(new RenderedCompactJsonFormatter(),"logs/log.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            Log.Information("No one listens to me!");


          
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(Log.Logger)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }
}
