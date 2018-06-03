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

        void AddTrip (Trip trip);
        Task<bool> SaveChangesAsync ();
    }
}