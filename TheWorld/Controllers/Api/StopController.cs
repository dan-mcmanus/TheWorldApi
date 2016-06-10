using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private CoordinateService coordinateService;
        private ILogger<StopController> logger;
        private IWorldRepository repository;

        public StopController(IWorldRepository _repository, 
            ILogger<StopController> _logger,
            CoordinateService _coordinateService)
        {
            repository = _repository;
            logger = _logger;
            coordinateService = _coordinateService;
        }

        [HttpGet("")]        
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = repository.GetTripByName(tripName, User.Identity.Name);
                if (results == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops.OrderBy(s => s.Order)));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get stops for trip {tripName}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error occurred finding trip name");

            }
        }

        public async Task<JsonResult> Post(string tripName, [FromBody]StopViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(model);

                    // Look up GeoCoordinates
                    var coordinateResult = await coordinateService.Lookup(newStop.Name);

                    if (!coordinateResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(coordinateResult.Message);
                    }

                    newStop.Longitude = coordinateResult.Longitude;
                    newStop.Latitude = coordinateResult.Latitude;

                    repository.AddStop(tripName, newStop);

                    if (repository.saveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<IEnumerable<StopViewModel>>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to save new Stop", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to Save new stop");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation Failed On New Stop");
        }
    }
}
