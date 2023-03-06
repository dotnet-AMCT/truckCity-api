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
        private IMapper _mapper;

        public TruckRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<TruckDto> CreateUpdate(TruckDto truckDto)
        {
            Truck truck = _mapper.Map<TruckDto, Truck>(truckDto);
            if (truck.Id > 0) 
            {
                _context.Trucks.Update(truck);
            }
            else
            {
                await _context.Trucks.AddAsync(truck);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<Truck, TruckDto>(truck);
        }

        public async Task<bool> DeleteTruck(int id)
        {
            try
            {
                Truck? truck = await _context.Trucks.FindAsync(id);
                if (truck == null)
                {
                    return false;
                }
                _context.Trucks.Remove(truck);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<TruckDto> GetTruckById(int id)
        {
            Truck? truck = await _context.Trucks.FindAsync(id);
            return _mapper.Map<TruckDto>(truck);
        }

        public async Task<List<TruckDto>> GetTrucks()
        {
            List<Truck> truckList = await _context.Trucks.ToListAsync();
            return _mapper.Map<List<TruckDto>>(truckList);
        }
    }
}
