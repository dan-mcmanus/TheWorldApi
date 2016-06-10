using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> logger;
        private IWorldRepository repository;

        public TripController(IWorldRepository _repository, ILogger<TripController> _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = repository.GetUserTripsWithStops(User.Identity.Name);
            var results = Mapper.Map<IEnumerable<TripViewModel>>(trips);

            return Json(results);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(model);
                    newTrip.UserName = User.Identity.Name;

                    logger.LogInformation("Attempting to save to a new trip");
                    repository.AddTrip(newTrip);

                    if (repository.saveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }


                }
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to save new trip", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState.Values });
        }
    }
}
