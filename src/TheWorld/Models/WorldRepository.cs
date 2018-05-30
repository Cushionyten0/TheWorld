using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository (WorldContext context, ILogger<WorldRepository> loggger)
        {
            _context = context;
            _logger = loggger;
        }

        public IEnumerable<Trip> GetAllTrips ()
        {
            _logger.LogInformation ("Getting All Trips from the Database");
            return _context.Trips.ToList ();
        }
    }
}