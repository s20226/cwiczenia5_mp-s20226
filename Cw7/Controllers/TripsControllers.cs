using Cw7.Models;
using Cw7.Services;
using Microsoft.AspNetCore.Mvc;
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



        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTrips();
            return Ok(trips);
        }

        [HttpPost]
        [Route("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(ClientTripRequest clientTripRequest)
        {
            if (!await _dbService.CheckClientByPesel(clientTripRequest.Pesel))
            {
                await _dbService.AddClient(new Client
                {
                    FirstName = clientTripRequest.FirstName,
                    LastName = clientTripRequest.LastName,
                    Email = clientTripRequest.Email,
                    Telephone = clientTripRequest.Telephone,
                    Pesel = clientTripRequest.Pesel


                });
            }
            var clientId = await _dbService.GetClientIdByPesel(clientTripRequest.Pesel);

            if (!await _dbService.CheckTrip(clientTripRequest.IdTrip))
            {
                return BadRequest($"Trip with {clientTripRequest.IdTrip} isn't exist");
            }


            if (await _dbService.CheckIfClientIsAssignedToTrip(clientId, clientTripRequest.IdTrip))
            {
                return BadRequest($"Client {clientId} is already assigned to trip {clientTripRequest.IdTrip}");
            }

            await _dbService.AssignClientToTrip(clientId, clientTripRequest.IdTrip, clientTripRequest.PaymentDate);


            return Ok("Client Assigned to trip");


        }


    }

}
