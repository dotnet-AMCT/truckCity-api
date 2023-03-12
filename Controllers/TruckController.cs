using System;
using System.Collections.Generic;
using System.Linq;
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

        public TruckController(ITruckRepository iTruckRepository)
        {
            _iTruckRepository = iTruckRepository;
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
        public async Task<ActionResult<TruckDto>> CreateItemAsync(CreateTruckDto truckDto)
        {
            Truck truck = new()
            {
                Id = Guid.NewGuid(),
                LicencePlate = truckDto.LicencePlate,
                Brand = truckDto.Brand,
                Model = truckDto.Model,
                Year = truckDto.Year,
                Kilometres = truckDto.Kilometres,
                IsSold = truckDto.IsSold,
                BrokenParts = truckDto.BrokenParts,
                CompatiblePartCodes = truckDto.CompatiblePartCodes,
                Plant = null //import repository plant
            };

            await _iTruckRepository.CreateTruckAsync(truck);

            return CreatedAtAction(nameof(GetTruckAsync), new { id = truck.Id }, truck.AsDto());
        }

        // PUT: api/Truck/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateTruckAsync(Guid id, UpdateTruckDto truckDto)
        //{
        //    var existingTruck = await _iTruckRepository.GetTruckAsync(id);

        //    if (existingTruck is null)
        //    {
        //        return NotFound();
        //    }

        //    existingTruck.Name = truckDto.Name;
        //    existingTruck.Price = truckDto.Price;

        //    await _iTruckRepository.UpdateTruckAsync(existingTruck);

        //    return NoContent();
        //}

        //// DELETE: api/Truck/5
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
