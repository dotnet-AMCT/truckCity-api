using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using truckCity_api.Data;
using truckCity_api.Models;
using truckCity_api.Models.Dto;

namespace truckCity_api.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly List<string> PartNames = new List<string> { "Radiator",
                                                                    "Brake group",
                                                                    "Engine",
                                                                    "Front frame",
                                                                    "Filters",
                                                                    "Fuel system",
                                                                    "Rear lights",
                                                                    "Front lights",
                                                                    "Number plate lights",
                                                                    "Clutch",
                                                                    "Rim",
                                                                    "Right door",
                                                                    "Left door",
                                                                    "Windshield" };

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public PartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartDto>> GetPart()
        {
            List<Part> partsList = await _db.Part.ToListAsync();

            return _mapper.Map<List<PartDto>>(partsList);
        }

        public async Task<PartDto> GetPart(Guid id)
        {
            var part = await _db.Part.FindAsync(id);

            return _mapper.Map<PartDto>(part);
        }

        public async Task<PartDto?> CreatePart(CreatePartDto createPartDto)
        {
            Part? part = null;
            if (PartNames.Contains(createPartDto.Name))
            {
                part = _mapper.Map<Part>(createPartDto);
                await _db.Part.AddAsync(part);
                await _db.SaveChangesAsync();
            }

            PartDto? partDto = part!=null ? _mapper.Map<PartDto>(part) : null;

            return partDto;
        }

        public async Task<PartDto?> UpdatePart(Guid id, UpdatePartDto updatePartDto)
        {
            PartDto? partDto = null;
            Part? part = await _db.Part.FindAsync(id);
            bool is_valid_new_name = updatePartDto.Name != null ? PartNames.Contains(updatePartDto.Name) : false;

            if (part != null && is_valid_new_name)
            {
                if (updatePartDto.Name != null)
                {
                    part.Name = updatePartDto.Name;
                }
                if (updatePartDto.Code != null)
                {
                    part.Code = updatePartDto.Code;
                }

                await _db.SaveChangesAsync();

                partDto = _mapper.Map<PartDto>(part);
            }

            return partDto;
        }

        public async Task<bool> DeletePart(Guid id)
        {
            var operationResult = true;

            try
            {
                var part = await _db.Part.FindAsync(id);
                if (part != null)
                {
                    _db.Part.Remove(part);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    operationResult = false;
                }
            }
            catch (Exception)
            {
                operationResult = false;
            }

            return operationResult;
        }

        public async Task<List<ReplacementPartDto>?> SearchPartsForReplacement(Guid truckId, List<string> names)
        {
            List<ReplacementPartDto>? availablePartsDto = null;
            
            // validate names list
            bool isValidNameList = true;
            foreach (var name in names)
            {
                isValidNameList = isValidNameList && PartNames.Contains(name);
            }

            var truck = await _db.Trucks.FindAsync(truckId);
            List<string>? truckCodesList = truck!=null ? truck.CompatiblePartCodes : null;

            if (isValidNameList && truckCodesList!=null)
            {
                availablePartsDto = new List<ReplacementPartDto>();

                var queryParts = _db.Part.AsQueryable();
                var availableParts = await queryParts
                                    .Where(p => p.Truck==null)
                                    .Where(p => truckCodesList.Contains(p.Code))
                                    .Where(p => names.Contains(p.Name))
                                    .GroupBy(p => new { p.Code, p.Name })
                                    .Select(g => g.First())
                                    .ToListAsync();

                foreach (var part in availableParts)
                {
                    availablePartsDto.Add(_mapper.Map<ReplacementPartDto>(part));
                }
            }

            return availablePartsDto;
        }

        public async Task<PartDto?> AssignOrUnassignTotruck(Guid id, Guid? truckId)
        {
            PartDto? partDto = null;
            var part = await _db.Part.FindAsync(id);
            
            if (part != null)
            {
                if (truckId != null)
                {
                    var truck = await _db.Trucks.FindAsync(truckId);
                    if (truck != null)
                    {
                        part.Truck = truck;
                        await _db.SaveChangesAsync();
                        partDto = _mapper.Map<PartDto>(part);
                    }
                }
                else
                {
                    part.TruckId = null;
                    await _db.SaveChangesAsync();
                    partDto = _mapper.Map<PartDto>(part);
                }
            }
            
            return partDto;
        }

        public async Task<PartStock?> SearchPartsByCode(string code)
        {
            PartStock? partStock = null;

            var query = await (from p in _db.Set<Part>()
                                where p.Code == code
                                group p by p.Name
                                into g
                                select new { Name = g.Key, StockAvailability = g.Count() })
                            .ToListAsync();

            if (query != null && query.Count() > 0)
            {
                partStock = new PartStock(
                                    query[0].Name, 
                                    query[0].StockAvailability
                                );
            }

            return partStock;
        }
    }
}
