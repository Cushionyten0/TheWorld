using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips ();
        Trip GetTripByName (string tripName);
        IEnumerable<Trip> GetTripsByUsername (string name);

        void AddTrip (Trip trip);
        void AddStop (string tripName, Stop newStop);

        Task<bool> SaveChangesAsync ();
    }
}