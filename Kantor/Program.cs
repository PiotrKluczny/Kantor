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
using Serilog.Formatting;
using Serilog.Formatting.Compact;

namespace Kantor
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", false, true)
                 // .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                 // .AddJsonFile("appsettings.local.json", true)
                  .Build();

            //  CreateHostBuilder(args).Build().Run();

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .MinimumLevel.Information()
            //dodalismy do logow ze logi z microsoftu beda dodawane do logow ale od poziomu warning
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //dodalismy do logow ze logi z systemu beda dodawane do logow ale od poziomu error
            .MinimumLevel.Override("System", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Console()
            .WriteTo.File("C:\\Logs\\log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            //         Log.Logger = new LoggerConfiguration()
            // .ReadFrom.Configuration(configuration)
            //.CreateLogger();

            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }
}
