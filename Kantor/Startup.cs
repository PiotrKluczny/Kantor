using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kantor.Client;
using Kantor.Interfaces;
using Kantor.Logic;
using Kantor.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static Kantor.Client.ResponseTable;


namespace Kantor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            using (var client = new NbpDbContext())
            {
                client.Database.EnsureCreated();
            }
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //dodalismy to do konfiguracji appsetting. zobaczymy czy dobrze
            var pathConfiguration =
                Configuration.GetSection("PathDirections");
            services.Configure<NbpFilePath>(pathConfiguration);

            services.AddControllers();
            //rejestracja bazy danych
            services.AddEntityFrameworkSqlite().AddDbContext<NbpDbContext>();
           
            //dodane services aby zarejestrowac interfajsy uzyte do apce
            services.AddTransient<INbpLogic, NbpLogic>();
            services.AddTransient<INbpFile, NbpFile>();
            services.AddTransient<INbpClient, NbpClient>();
            services.AddTransient<INbpCurrencyLogic, NbpCurrencyLogic>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
       
    }
}
