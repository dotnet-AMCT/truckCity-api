using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using truckCity_api.Data;
using truckCity_api.Models;
using truckCity_api.Models.Dto;
using truckCity_api.Repositories;
using truckCity_api.Utilities;

namespace truckCity_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly ITruckRepository _iTruckRepository;
        private readonly IPlantRepository _iPlantRepository;

        public TruckController(ITruckRepository iTruckRepository, IPlantRepository iPlantRepository)
        {
            _iTruckRepository = iTruckRepository;
            _iPlantRepository = iPlantRepository;
        }

        // GET: api/Truck
        [HttpGet]
        public async Task<IEnumerable<TruckDto>> GetTrucksAsync()
        {
            var trucks = (await _iTruckRepository.GetTrucksAsync())
                        .Select(truck => truck.AsDto());

            return trucks;
        }

        [ActionName("GetTruckAsync")]
        // GET: api/Truck/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TruckDto>> GetTruckAsync(Guid id)
        {
            var truck = await _iTruckRepository.GetTruckAsync(id);

            if (truck is null)
            {
                return NotFound();
            }

            return truck.AsDto();
        }

        // POST: api/Truck
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TruckDto>> CreateTruckAsync(CreateTruckDto truckDto)
        {
            Truck truck = new()
            {
                LicencePlate = truckDto.LicencePlate,
                Brand = truckDto.Brand,
                Model = truckDto.Model,
                Year = truckDto.Year,
                Kilometres = truckDto.Kilometres,
                IsSold = truckDto.IsSold
            };

            try
            {
                await _iTruckRepository.CreateTruckAsync(truck);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return CreatedAtAction(nameof(GetTruckAsync), new { id = truck.Id }, truck.AsDto());
        }

        // PUT: api/Truck/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTruckAsync(Guid id, UpdateTruckDto truckDto)
        {
            var existingTruck = await _iTruckRepository.GetTruckAsync(id);

            if (existingTruck is null)
            {
                return NotFound();
            }

            PropertyInfo[] truckDtoProperties = typeof(UpdateTruckDto).GetProperties();
            PropertyInfo[] truckProperties = typeof(Truck).GetProperties();

            foreach (PropertyInfo propTruckDto in truckDtoProperties)
            {
                if (propTruckDto.GetValue(truckDto, null) != null)
                {
                    foreach (PropertyInfo propTruck in truckProperties)
                    {
                        if (propTruck.Name == propTruckDto.Name)
                            propTruck.SetValue(existingTruck, propTruckDto.GetValue(truckDto, null), null);
                    }
                }
            }

            await _iTruckRepository.UpdateTruckAsync(existingTruck);

            return NoContent();
        }

        // PUT: api/Truck/SetCompatiblePartCodes/5
        [HttpPut("SetCompatiblePartCodes/{id}")]
        public async Task<ActionResult> SetCompatiblePartCodesAsync(Guid id, List<string> CompatiblePartCodes)
        {
            var existingTruck = await _iTruckRepository.GetTruckAsync(id);

            if (existingTruck is null)
            {
                return NotFound();
            }

            if (!existingTruck.CompatiblePartCodes.Any())
            {
                existingTruck.CompatiblePartCodes = CompatiblePartCodes;
            }
            else
            {
                foreach (string partCode in CompatiblePartCodes)
                {
                    if (!existingTruck.CompatiblePartCodes.Contains(partCode))
                    {
                        existingTruck.CompatiblePartCodes.Add(partCode);
                    }
                }
            }

            await _iTruckRepository.UpdateTruckAsync(existingTruck);

            return NoContent();
        }

        // DELETE: api/Truck/DeleteCompatiblePartCodes/5
        [HttpDelete("DeleteCompatiblePartCodes/{id}")]
        public async Task<ActionResult> DeleteCompatiblePartCodesAsync(Guid id, List<string> CompatiblePartCodes)
        {
            var existingTruck = await _iTruckRepository.GetTruckAsync(id);

            if (existingTruck is null)
            {
                return NotFound();
            }

            if (!existingTruck.CompatiblePartCodes.Any())
            {
                return NoContent();
            }
            else
            {
                foreach (string partCode in CompatiblePartCodes)
                {
                    if (existingTruck.CompatiblePartCodes.Contains(partCode))
                    {
                        existingTruck.CompatiblePartCodes.Remove(partCode);
                    }
                }
            }

            await _iTruckRepository.UpdateTruckAsync(existingTruck);

            return NoContent();
        }

        // PUT: api/Truck/SetBrokenParts/5
        [HttpPut("SetBrokenParts/{id}")]
        public async Task<ActionResult> SetBrokenPartsAsync(Guid id, List<string> BrokenParts)
        {
            var existingTruck = await _iTruckRepository.GetTruckAsync(id);

            if (existingTruck is null)
            {
                return NotFound();
            }

            if (!existingTruck.BrokenParts.Any())
            {
                existingTruck.BrokenParts = BrokenParts;
            }
            else
            {
                foreach (string brokenPart in BrokenParts)
                {
                    if (!existingTruck.BrokenParts.Contains(brokenPart))
                    {
                        existingTruck.BrokenParts.Add(brokenPart);
                    }
                }
            }

            await _iTruckRepository.UpdateTruckAsync(existingTruck);

            return NoContent();
        }

        // DELETE: api/Truck/DeleteBrokenParts/5
        [HttpDelete("DeleteBrokenParts/{id}")]
        public async Task<ActionResult> DeleteBrokenPartsAsync(Guid id, List<string> BrokenParts)
        {
            var existingTruck = await _iTruckRepository.GetTruckAsync(id);

            if (existingTruck is null)
            {
                return NotFound();
            }

            if (!existingTruck.BrokenParts.Any())
            {
                return NoContent();
            }
            else
            {
                foreach (string brokenPart in BrokenParts)
                {
                    if (existingTruck.BrokenParts.Contains(brokenPart))
                    {
                        existingTruck.BrokenParts.Remove(brokenPart);
                    }
                }
            }

            await _iTruckRepository.UpdateTruckAsync(existingTruck);

            return NoContent();
        }

        // PUT: api/Truck/SetPlant/5
        [HttpPut("SetPlant/{id}")]
        public async Task<ActionResult> SetPlantIdAsync(Guid id, Guid? PlantId)
        {
            var existingTruck = await _iTruckRepository.GetTruckAsync(id);

            if (existingTruck is null)
            {
                return NotFound();
            }

            if (PlantId.HasValue)
            {
                var existingPlant = await _iPlantRepository.GetPlantAsync(PlantId.Value);

                if (existingPlant is null)
                {
                    return NotFound();
                }

                if (existingPlant.CurrentCapacity == existingPlant.MaxCapacity)
                {
                    return BadRequest(new { message = "The Plant in question is already full" });
                }

                if (existingTruck.PlantId is not null)
                {
                    var previousPlant = await _iPlantRepository.GetPlantAsync(existingTruck.PlantId.Value);
                    previousPlant.CurrentCapacity -= 1;
                    await _iPlantRepository.UpdatePlantAsync(previousPlant);
                }

                existingPlant.CurrentCapacity += 1;
                existingTruck.Plant = existingPlant;
                await _iPlantRepository.UpdatePlantAsync(existingPlant);
            }
            else
            {
                if (existingTruck.PlantId is null)
                {
                    return NoContent();
                }
                else
                {
                    var previousPlant = await _iPlantRepository.GetPlantAsync(existingTruck.PlantId.Value);
                    previousPlant.CurrentCapacity -= 1;
                    existingTruck.PlantId = PlantId;
                    await _iPlantRepository.UpdatePlantAsync(previousPlant);
                }
            }

            await _iTruckRepository.UpdateTruckAsync(existingTruck);
            return NoContent();
        }

        // DELETE: api/Truck/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTruckAsync(Guid id)
        {
            var existingTruck = await _iTruckRepository.GetTruckAsync(id);

            if (existingTruck is null)
            {
                return NotFound();
            }

            await _iTruckRepository.DeleteTruckAsync(id);

            return NoContent();
        }
    }
}
