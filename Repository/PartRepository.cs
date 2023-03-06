using AutoMapper;
using Microsoft.EntityFrameworkCore;
using truckCity_api.Data;
using truckCity_api.Models;
using truckCity_api.Models.DTO;

namespace truckCity_api.Repository
{
    public class PartRepository : IPartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PartRepository(ApplicationDbContext db, Mapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> DeletePart(int id)
        {
            var operationResult = true;
            var part = await _db.Part.FindAsync(id);

            if (part == null)
            {
                operationResult = false;
            }

            try
            {
                _db.Part.Remove(part);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                operationResult= false;
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

        public async Task<PartDTO> PostPutPart(PartDTO partDTO)
        {
            var part = _mapper.Map<Part>(partDTO);

            // If it's an update
            if (part.Id > 0)
            {
                _db.Part.Update(part);
            }
            else
            {
                await _db.Part.AddAsync(part);
            }
            
            await _db.SaveChangesAsync();

            return partDTO;
        }
    }
}
