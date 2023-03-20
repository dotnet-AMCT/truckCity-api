using truckCity_api.Models;

namespace truckCity_api.Repositories
{
    public interface IPlantRepository
    {
        Task<IEnumerable<Plant>> GetPlantsAsync();
        Task<Plant> GetPlantAsync(Guid id);
        Task CreatePlantAsync(Plant plant);
        Task UpdatePlantAsync(Plant plant);
        Task DeletePlantAsync(Guid id);
    }
}
