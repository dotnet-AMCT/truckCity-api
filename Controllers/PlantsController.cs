using Microsoft.AspNetCore.Mvc;
using truckCity_api.Utilities;
using truckCity_api.Models.DTO;
using truckCity_api.Repositories;
using truckCity_api.Models;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Configuration;

namespace truckCity_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly IPlantRepository _iPlantRepository;

        public PlantsController(IPlantRepository iPlantRepository)
        {
            _iPlantRepository = iPlantRepository;
        }

        // GET: api/Plants
        [HttpGet]
        public async Task<IEnumerable<PlantDto>> GetPlantsAsync()
        {
            var plants = (await _iPlantRepository.GetPlantsAsync())
                        .Select(plants => plants.AsDto());

            return plants;
        }

        // GET: api/Plants/5
        [ActionName("GetPlantAsync")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantDto>> GetPlantAsync(Guid id)
        {
            var plant = await _iPlantRepository.GetPlantAsync(id);

            if (plant is null)
            {
                return NotFound();
            }

            return plant.AsDto();
        }

        // PUT: api/Plants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlantAsync(Guid id, UpdatePlantDto plantDto)
        {
            var existingPlant = await _iPlantRepository.GetPlantAsync(id);

            if (existingPlant is null)
            {
                return NotFound();
            }

            PropertyInfo[] plantDtoProperties = typeof(UpdatePlantDto).GetProperties();
            PropertyInfo[] plantProperties = typeof(Plant).GetProperties();
            foreach (PropertyInfo propPlantDto in plantDtoProperties)
            {
                if (propPlantDto.GetValue(plantDto, null) != null)
                {
                    foreach (PropertyInfo propPlant in plantProperties)
                    {
                        if (propPlant.Name == propPlantDto.Name)
                            propPlant.SetValue(existingPlant, propPlantDto.GetValue(plantDto, null), null);
                    }
                }
            }

            await _iPlantRepository.UpdatePlantAsync(existingPlant);

            return NoContent();
        }

        // POST: api/Plants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlantDto>> CreatePlantAsync(CreatePlantDto plantDto)
        {
            Plant plant = new()
            {
                Id = Guid.NewGuid(),
                Name = plantDto.Name,
                MaxCapacity = plantDto.MaxCapacity,
                CurrentCapacity = plantDto.CurrentCapacity,
                Address = plantDto.Address
            };

            await _iPlantRepository.CreatePlantAsync(plant);

            return CreatedAtAction(nameof(GetPlantAsync), new { id = plant.Id }, plant.AsDto());
        }

        // DELETE: api/Plants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlantAsync(Guid id)
        {
            var existingPlant = await _iPlantRepository.GetPlantAsync(id);

            if (existingPlant is null)
            {
                return NotFound();
            }

            await _iPlantRepository.DeletePlantAsync(id);

            return NoContent();
        }
    }
}
