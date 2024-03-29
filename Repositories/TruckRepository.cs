using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using truckCity_api.Data;
using truckCity_api.Models;
using truckCity_api.Models.Dto;
using truckCity_api.Repositories;

namespace truckCity_api.Repositories
{
    public class TruckRepository : ITruckRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public TruckRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Truck>> GetTrucksAsync()
        {
            return await Task.FromResult(_context.Trucks);
        }

        public async Task<Truck?> GetTruckAsync(Guid id)
        {
            var truck = _context.Trucks.Where(truck => truck.Id == id).SingleOrDefault();
            return await Task.FromResult(truck);
        }

        public async Task CreateTruckAsync(Truck truck)
        {
            if (_context.Trucks.Any(x => x.LicencePlate == truck.LicencePlate))
                throw new Exception("Truck with Licence Plate '" + truck.LicencePlate + "' already exists");
            await _context.Trucks.AddAsync(truck);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task UpdateTruckAsync(Truck truck)
        {
            _context.Trucks.Update(truck);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task DeleteTruckAsync(Guid id)
        {
            var truck = _context.Trucks.Where(truck => truck.Id == id).SingleOrDefault();
            if (truck is not null)
            {
                if (truck.PlantId is not null)
                {
                    var plant = _context.Plants.Where(plant => plant.Id == truck.PlantId).SingleOrDefault();
                    if (plant is not null)
                    {
                        plant.CurrentCapacity -= 1;
                        _context.Plants.Update(plant);
                    }
                }
                _context.Trucks.Remove(truck);
                _context.SaveChanges();
            }
            await Task.CompletedTask;
        }

        public async Task SetPlantIdAsync(Guid id, Guid? PlantId)
        {
            var existingTruck = await GetTruckAsync(id);

            if (existingTruck is null)
            {
                throw new ArgumentNullException($"Truck id '{id}' does not exist");
            }

            if (PlantId.HasValue)
            {
                var existingPlant = _context.Plants.Where(plant => plant.Id == PlantId.Value).SingleOrDefault();

                if (existingPlant is null)
                {
                    throw new ArgumentNullException($"Plant id '{PlantId.Value}' does not exist");
                }

                if (existingPlant.CurrentCapacity == existingPlant.MaxCapacity)
                {
                    throw new InvalidOperationException("The Plant in question is already full");
                }

                if (existingTruck.PlantId is not null)
                {
                    var previousPlant = _context.Plants.Where(plant => plant.Id == existingTruck.PlantId.Value).SingleOrDefault();
                    if (previousPlant is not null)
                    {
                        previousPlant.CurrentCapacity -= 1;
                        _context.Plants.Update(previousPlant);
                    }
                }

                existingPlant.CurrentCapacity += 1;
                existingTruck.Plant = existingPlant;
                _context.Plants.Update(existingPlant);
            }
            else
            {
                if (existingTruck.PlantId is not null)
                {
                    var previousPlant = _context.Plants.Where(plant => plant.Id == existingTruck.PlantId.Value).SingleOrDefault();
                    if (previousPlant is not null)
                    {
                        previousPlant.CurrentCapacity -= 1;
                        existingTruck.PlantId = PlantId;
                        _context.Plants.Update(previousPlant);
                    }
                }
            }

            _context.Trucks.Update(existingTruck);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task<List<TruckDto>?> GetTrucksToSell(FilterTrucksToSellDto filters)
        {
            List<TruckDto>? trucksToSell = null;

            bool yearsBothNull = filters.MinYear==null && filters.MaxYear==null;
            bool validYears = yearsBothNull || (!yearsBothNull && filters.MinYear<=filters.MaxYear);

            if (validYears) {
                var allAvailableTrucks = await (from t in _context.Set<Truck>()
                                                where t.IsSold == false
                                                select new {
                                                    t.Id,
                                                    t.LicencePlate,
                                                    t.Brand,
                                                    t.Model,
                                                    t.Year,
                                                    t.Kilometres,
                                                    t.IsSold,
                                                    t.PlantId,
                                                    t.BrokenParts,
                                                    t.CompatiblePartCodes
                                                })
                                             .ToListAsync();

                if (allAvailableTrucks != null)
                {
                    trucksToSell = new List<TruckDto>();

                    foreach (var truck in allAvailableTrucks)
                    {
                        var truckPassesTheFilter = true;
                        if (truckPassesTheFilter && filters.Brand!=null)
                        {
                            truckPassesTheFilter = truckPassesTheFilter && truck.Brand==filters.Brand;
                        }
                        if (truckPassesTheFilter && filters.Model!=null)
                        {
                            truckPassesTheFilter = truckPassesTheFilter && truck.Model==filters.Model;
                        }
                        if (truckPassesTheFilter && !yearsBothNull)
                        {
                            truckPassesTheFilter = truckPassesTheFilter &&
                                (filters.MinYear <= truck.Year && truck.Year <= filters.MaxYear);
                        }
                        if (truckPassesTheFilter && filters.Kilometers != null)
                        {
                            truckPassesTheFilter = truckPassesTheFilter && truck.Kilometres<filters.Kilometers;
                        }

                        if (truckPassesTheFilter)
                            trucksToSell.Add(
                                new TruckDto(
                                    truck.Id,
                                    truck.LicencePlate,
                                    truck.Brand,
                                    truck.Model,
                                    truck.Year,
                                    truck.Kilometres,
                                    truck.IsSold,
                                    truck.PlantId,
                                    truck.BrokenParts,
                                    truck.CompatiblePartCodes
                                )
                            );
                    }
                }
            }

            return trucksToSell;
        }
    }
}
