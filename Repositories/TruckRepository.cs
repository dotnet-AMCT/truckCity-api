using AutoMapper;
using Microsoft.EntityFrameworkCore;
using truckCity_api.Data;
using truckCity_api.Models;
using truckCity_api.Models.Dto;
using truckCity_api.Repositories;

namespace truckCity_api.Repositories
{
    public class TruckRepository : ITruckRepository
    {
        private readonly ApplicationDbContext _context;

        public TruckRepository(ApplicationDbContext context)
        {
            _context = context;
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
            _context.Trucks.Remove(truck);
            _context.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
