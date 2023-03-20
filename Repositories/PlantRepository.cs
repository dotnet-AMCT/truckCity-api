using truckCity_api.Data;
using truckCity_api.Models;

namespace truckCity_api.Repositories
{
    public class PlantRepository: IPlantRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plant>> GetPlantsAsync()
        {
            return await Task.FromResult(_context.Plants);
        }

        public async Task<Plant> GetPlantAsync(Guid id)
        {
            var plant = _context.Plants.Where(plant => plant.Id == id).SingleOrDefault();
            return await Task.FromResult(plant);
        }

        public async Task CreatePlantAsync(Plant plant)
        {
            await _context.Plants.AddAsync(plant);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task UpdatePlantAsync(Plant plant)
        {
            _context.Plants.Update(plant);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task DeletePlantAsync(Guid id)
        {
            var plant = _context.Plants.Where(plant => plant.Id == id).SingleOrDefault();
            _context.Plants.Remove(plant);
            _context.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
