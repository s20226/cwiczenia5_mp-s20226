using Cw7.Models;
using Cw7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsControllers : ControllerBase
    {
        private readonly IDbService _dbService;
        public TripsControllers(IDbService dbService)
        {
            _dbService = dbService;
        }



        [HttpGet] //sortowanie do zrobienia
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTrips();
            return Ok(trips);
        }

        [HttpPost]
        [Route("{idTrip/clients")]
        public async Task<IActionResult> AssignClientToTrip(ClientTripRequest clientTripRequest)
        {

        }


    }

}
