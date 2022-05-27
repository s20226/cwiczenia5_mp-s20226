using Cw7.Models;
using Cw7.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.Services
{
    public class DbService : IDbService
    {
        private readonly MasterContext _dbContext;
        public DbService(MasterContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<object>> GetTrips()
        {
            return await _dbContext.Trips
                .Include(e => e.CountryTrips)
                .Include(e => e.ClientTrips)
                .Select(e => new SomeSortOfTrip
                {
                    Name = e.Name,
                    Description = e.Description,
                    MaxPeople = e.MaxPeople,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    Countries = e.CountryTrips.Select(e => new SomeSortOfCountry { Name = e.IdCountryNavigation.Name }).ToList(),
                    Clients = e.ClientTrips.Select(e => new SomeSortOfClient { FirstName = e.IdClientNavigation.FirstName, LastName = e.IdClientNavigation.LastName }).ToList()

                }).OrderByDescending(e => e.DateFrom).ToListAsync();

        }

        public async Task<bool> CheckIfClientIsAssignedToTrip(int idClient, int idTrip)
        {
            return await _dbContext.ClientTrips.AnyAsync(e => e.IdClient == idClient && e.IdTrip == idTrip);

        }

        public async Task<bool> CheckClientByPesel(string pesel)
        {
            return await _dbContext.Clients.AnyAsync(e => e.Pesel.Equals(pesel));
        }


        public async Task AddClient(Client client)
        {
            await _dbContext.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<bool> CheckTrip(int idTrip)
        {
            return await _dbContext.Trips.AnyAsync(e => e.IdTrip == idTrip);
        }

        public async Task<bool> RemoveClient(int idClient)
        {
            var client = new Client() { IdClient = idClient };


            if (await _dbContext.ClientTrips.AnyAsync(e => e.IdClient == idClient))
            {
                return false;
            }



            _dbContext.Attach(client);
            _dbContext.Remove(client);

            // var trip = await _dbContext.Trips.Where(e => e.IdTrip == id).FirstOrDefaultAsync();

            //var trip = new Trip() { IdTrip = id };
            ////dodawanie
            //var addTrip = new Trip { IdTrip = id, Name = "nazwaWycieczki" };
            //_dbContext.Add(addTrip);


            ////edy
            //var editTrip = await _dbContext.Trips.Where(e => e.IdTrip == id).FirstOrDefault();
            //editTrip.Name = "aaa";


            ///
            //_dbContext.Attach(trip);
            //_dbContext.Remove(trip);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task AssignClientToTrip(int idClient, int idTrip, DateTime? paymentDate)
        {
            
            var clientTrip = new ClientTrip
            {
                IdClient = idClient,
                IdTrip = idTrip,
                PaymentDate = paymentDate,
                RegisteredAt = DateTime.Now
            };

            await _dbContext.AddAsync(clientTrip);

            await _dbContext.SaveChangesAsync();

        }

        public async Task<int> GetClientIdByPesel(string pesel)
        {

            var id = await _dbContext.Clients.Where(e => e.Pesel.Equals(pesel)).Select(c => new
            {
                idClient = c.IdClient
            }).FirstOrDefaultAsync();


            return id.idClient;
        }
    }
}
