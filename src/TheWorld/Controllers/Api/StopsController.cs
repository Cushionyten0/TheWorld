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
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route ("api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<StopsController> _logger;
        private GeoCoordsService _coordsService;

        public StopsController (IWorldRepository repository, ILogger<StopsController> logger, GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet ("")]
        public IActionResult Get (string tripName)
        {
            try
            {
                var trip = _repository.GetUserTripByName (tripName, User.Identity.Name);
                return Ok (Mapper.Map<IEnumerable<StopViewModel>> (trip.Stops.OrderBy (s => s.Order).ToList ()));
            }
            catch (Exception e)
            {
                _logger.LogError ("Failed to get stops: {0}", e);
            }
            return BadRequest ("Failed to get stops");
        }

        [HttpPost ("")]
        public async Task<IActionResult> Post (string tripName, [FromBody] StopViewModel vm)
        {
            try
            {
                // If the VM is valid
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop> (vm);

                    // Lookup the geo\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\codes
                    var result = await _coordsService.GetCoordsAsync (newStop.Name);
                    if (!result.Success)
                    {
                        _logger.LogError (result.Message);
                    }
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;

                        // Save to the Database
                        _repository.AddStop (tripName, newStop, User.Identity.Name);

                        if (await _repository.SaveChangesAsync ())
                        {
                            return Created ($"/api/trip/{tripName}/stops/{newStop.Name}",
                                Mapper.Map<StopViewModel> (newStop));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError ("Failed to save a new stop {0}", e);
            }
            return BadRequest ("Failed to save a new stop");
        }
    }
}