using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using truckCity_api.Models.Dto;
using truckCity_api.Models;


namespace truckCity_api.Repositories
{
    public interface IPartRepository
    {
        Task<IEnumerable<PartDto>> GetPart();
        Task<PartDto> GetPart(Guid id);
        Task<PartDto?> CreatePart(CreatePartDto createPartDto);
        Task<PartDto?> UpdatePart(Guid id, UpdatePartDto updatePartDto);
        Task<bool> DeletePart(Guid id);
        Task<List<ReplacementPartDto>?> SearchPartsForReplacement(Guid truckId, List<string> names);
        Task<PartDto?> AssignOrUnassignTotruck(Guid id, Guid? truckId);
        Task<PartStock?> SearchPartsByCode(string code);
    }
}