using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using truckCity_api.Models.DTO;
using truckCity_api.Models;


namespace truckCity_api.Repositories
{
    public interface IPartRepository
    {
        Task<IEnumerable<PartDTO>> GetPart();
        Task<PartDTO> GetPart(Guid id);
        Task<PartDTO?> CreatePart(CreatePartDTO createPartDTO);
        Task<PartDTO?> UpdatePart(Guid id, UpdatePartDTO updatePartDTO);
        Task<bool> DeletePart(Guid id);
        Task<List<ReplacementPartDTO>?> SearchPartsForReplacement(Guid truckId, List<string> names);
        Task<PartDTO?> AssignOrUnassignTotruck(Guid id, Guid? truckId);
    }
}