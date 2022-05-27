using Cw7.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cw7.Services
{
    public interface IDbService
    {
        public Task<IEnumerable<object>> GetTrips();
        public Task<bool> RemoveClient(int id);
        public Task AssignClientToTrip(int idClient, int idTrip, DateTime? paymentDate);
        public Task<bool> CheckIfClientIsAssignedToTrip(int idClient, int idTrip);
        public Task<bool> CheckClientByPesel(string pesel);
        public Task AddClient(Client client);
        public Task<bool> CheckTrip(int id);
        public Task<int> GetClientIdByPesel(string pesel);

    }
}
