﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using truckCity_api.Data;
using truckCity_api.Models;
using truckCity_api.Models.DTO;

namespace truckCity_api.Repository
{
    public class PartRepository : IPartRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public PartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> DeletePart(int id)
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

        public async Task<IEnumerable<PartDTO>> GetPart()
        {
            List<Part> partsList = await _db.Part.ToListAsync();

            return _mapper.Map<List<PartDTO>>(partsList);
        }

        public async Task<PartDTO> GetPart(int id)
        {
            var part = await _db.Part.FindAsync(id);

            return _mapper.Map<PartDTO>(part);
        }

        public async Task<PartDTO> CreatePart(PartDTO partDTO)
        {
            var part = _mapper.Map<Part>(partDTO);
            await _db.Part.AddAsync(part);
            await _db.SaveChangesAsync();

            return _mapper.Map<PartDTO>(part);
        }

        public async Task<PartDTO?> UpdatePart(int id, UpdatePartDTO updatePartDTO)
        {
            PartDTO? partDTO = null;
            Part? part = await _db.Part.FindAsync(id);

            if (part != null)
            {
                if (updatePartDTO.Name != null)
                {
                    part.Name = updatePartDTO.Name;
                }
                if (updatePartDTO.Code != null)
                {
                    part.Code = updatePartDTO.Code;
                }
                if (updatePartDTO.TruckId != null)
                {
                    part.TruckId = updatePartDTO.TruckId;
                }

                await _db.SaveChangesAsync();

                partDTO = _mapper.Map<PartDTO>(part);
            }

            return partDTO;
        }
    }
}
