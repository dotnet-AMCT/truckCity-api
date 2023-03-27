using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
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

        public async Task<Truck> GetTruckAsync(Guid id)
        {
            var truck = _context.Trucks.Where(truck => truck.Id == id).SingleOrDefault();
            return await Task.FromResult(truck);
        }

        public async Task CreateTruckAsync(Truck truck)
        {
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
            _context.Trucks.Remove(truck);
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
