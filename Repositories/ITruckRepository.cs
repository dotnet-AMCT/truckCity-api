using truckCity_api.Models;
using truckCity_api.Models.Dto;

namespace truckCity_api.Repositories
{
    public interface ITruckRepository
    {
        Task<IEnumerable<Truck>> GetTrucksAsync();
        Task<Truck> GetTruckAsync(Guid id);
        Task CreateTruckAsync(Truck truck);
    //    Task UpdateTruckAsync(Guid id, Truck truck);
        Task DeleteTruckAsync(Guid id);
    }
}
