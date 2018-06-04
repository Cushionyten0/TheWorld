using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public void AddTrip (Trip trip)
        {
            _context.Add (trip);
        }

        public async Task<bool> SaveChangesAsync ()
        {
            return (await _context.SaveChangesAsync ()) > 0;
        }

        public Trip GetTripByName (string tripName)
        {
            return _context.Trips
                .Include (t => t.Stops)
                .Where (t => t.Name == tripName)
                .FirstOrDefault ();
        }

        public void AddStop (string tripName, Stop newStop)
        {
            var trip = GetTripByName (tripName);
            if (trip != null)
            {
                trip.Stops.Add (newStop); //foreign keys are set
                _context.Stops.Add (newStop); //adds the item to the database
            }
        }
    }
}