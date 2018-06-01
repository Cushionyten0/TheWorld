using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route ("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;

        public TripsController (IWorldRepository repository)
        {
            _repository = repository;
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
                return BadRequest ("Error Occured");
            }
        }

        [HttpPost ("")]
        public IActionResult Post ([FromBody] TripViewModel theTrip)
        {
            var newTrip = Mapper.Map<Trip> (theTrip);

            if (ModelState.IsValid)
            {
                return Created ($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel> (newTrip));
            }
            return BadRequest (ModelState);
        }
    }
}