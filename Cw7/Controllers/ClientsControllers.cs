using Cw7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsControllers : ControllerBase
    {
        private readonly IDbService _dbService;
        public ClientsControllers(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpDelete]
        [Route("{idClient}")]
        public async Task<IActionResult> RemoveClient(int idClient)
        {
            if (!await _dbService.RemoveClient(idClient))
            {
                return BadRequest("Client have trips, cant be deleted");
            }
            return Ok("Client removed");
        }

    }
}
