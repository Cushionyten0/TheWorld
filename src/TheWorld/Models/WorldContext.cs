using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheWorld.Models
{
    public class WorldContext : IdentityDbContext<WorldUser>
    {
        private IConfigurationRoot _config;
        private IHostingEnvironment _env;

        public WorldContext (IConfigurationRoot config, IHostingEnvironment env, DbContextOptions options) : base (options)
        {
            _config = config;
            _env = env;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring (optionsBuilder);
            if (_env.IsEnvironment ("RemoteDev") || _env.IsEnvironment ("Remote"))
            {
                optionsBuilder.UseMySql (_config["ConnectionString:RemoteContextConnection"]);
            }
            else if (_env.IsEnvironment ("Testing") || _env.IsEnvironment ("Production"))
            {
                optionsBuilder.UseMySql (Helpers.GetRDSConnectionString ());
            }
            else if (_env.IsEnvironment ("Development") || _env.IsEnvironment ("Local"))
            {
                optionsBuilder.UseMySql (_config["ConnectionString:WorldContextConnection"]);
            }
        }
    }
}