using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route ("/api/trips")]
    [Authorize]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<TripsController> _logger;

        public TripsController (IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet ("")]
        public IActionResult Get ()
        {
            try
            {
                var results = _repository.GetAllTrips ();

                return Ok (Mapper.Map<IEnumerable<TripViewModel>> (results));

            }
            catch (Exception e)
            {
                // TODO Logging
                _logger.LogError ($"Failed to get All Trips: {e}");
                return BadRequest ("Error Occured");
            }
        }

        [HttpPost ("")]
        public async Task<IActionResult> Post ([FromBody] TripViewModel theTrip)
        {
            var newTrip = Mapper.Map<Trip> (theTrip);
            _repository.AddTrip (newTrip);

            if (ModelState.IsValid)
            {
                if (await _repository.SaveChangesAsync ())
                {
                    return Created ($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel> (newTrip));
                }
            }
            return BadRequest ("Failed to save changes to the Database");
        }
    }
}