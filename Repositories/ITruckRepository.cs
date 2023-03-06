using truckCity_api.Models.Dto;

namespace truckCity_api.Repositories
{
    public interface ITruckRepository
    {
        Task<List<TruckDto>> GetTrucks();

        Task<TruckDto> GetTruckById(int id);

        Task<TruckDto> CreateUpdate(TruckDto truckDto);

        Task<bool> DeleteTruck(int id);

    }
}
