using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
                if (updatePartDto.TruckId != null)
                {
                    part.TruckId = updatePartDto.TruckId;
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
    }
}
