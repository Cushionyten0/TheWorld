using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace src
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup (IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder ()
                .SetBasePath (_env.ContentRootPath)
                .AddJsonFile ("config.json")
                .AddJsonFile ($"secrets.json", optional : true)
                .AddEnvironmentVariables ();

            _config = builder.Build ();
        }

        public void ConfigureServices (IServiceCollection services)
        {

            services.AddMvc (config =>
                {
                    //If you attempt to go to HTTP it will redirect to HTTPS
                    if (_env.IsProduction ())
                    {
                        config.Filters.Add (new RequireHttpsAttribute ());
                    }
                })
                .AddJsonOptions (opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
                });

            services.AddSingleton (_config);

            if (_env.IsEnvironment ("Development") || _env.IsEnvironment ("Testing") ||
                _env.IsEnvironment ("RemoteDev"))
            {
                services.AddScoped<IMailService, DebugMailService> ();
            }
            else
            {
                //Implement a real Mail Service
            }

            services.AddIdentity<WorldUser, IdentityRole> (config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<WorldContext> ();
            services.ConfigureApplicationCookie (options => options.LoginPath = "/Auth/Login");

            services.AddLogging ();

            services.AddDbContext<WorldContext> ();
            services.AddScoped<IWorldRepository, WorldRepository> ();
            services.AddTransient<GeoCoordsService> ();
            services.AddTransient<WorldContextSeedData> ();

            //services.AddTransient<WorldContextSeedData> ();
            // _context.Database.Migrate()
            // Database.EnsureCreated();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app,
            IHostingEnvironment env,
            WorldContextSeedData seeder,
            ILoggerFactory factory)
        {
            //app.UseDefaultFiles (); //Feeds index.html into UseStaticFiles call
            app.UseStaticFiles (); //These two don't work in reverse

            app.UseIdentity ();

            Mapper.Initialize (config =>
            {
                config.CreateMap<TripViewModel, Trip> ().ReverseMap ();
                config.CreateMap<StopViewModel, Stop> ().ReverseMap ();
            });

            if (env.IsEnvironment ("Development") || env.IsEnvironment ("Testing") ||
                env.IsEnvironment ("RemoteDev"))
            {
                app.UseDeveloperExceptionPage ();
                factory.AddDebug (LogLevel.Information);
            }
            else
            {
                factory.AddDebug (LogLevel.Error);
            }

            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory> ()
                    .CreateScope ())
                {

                    serviceScope.ServiceProvider.GetService<WorldContext> ()
                        .Database.EnsureCreated ();
                }
            }
            catch (Exception e)
            {
                var msg = e.Message;
                var stacktrace = e.StackTrace;
            }

            app.UseMvc (config =>
            {
                config.MapRoute (
                    name: "Default",
                    template: "{controller}/{action}/{id?}", //id? doesn't have to exist 
                    defaults : new { controller = "App", action = "Index" } //action = "Index"; to specify which method 
                );
            });

            seeder.EnsureSeedData ().Wait ();
        }
    }
}