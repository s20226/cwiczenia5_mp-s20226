using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.Services
{
    public interface IDbService
    {
        Task<IEnumerable<object>> GetTrips();
        Task<bool> RemoveClient(int id);
        Task<> AssigneClientToTrip()
    }
}
