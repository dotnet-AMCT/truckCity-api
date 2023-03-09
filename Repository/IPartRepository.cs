using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using truckCity_api.Models.DTO;


namespace truckCity_api.Repository
{
    public interface IPartRepository
    {
        Task<IEnumerable<PartDTO>> GetPart();
        Task<PartDTO> GetPart(int id);
        Task<PartDTO> CreatePart(PartDTO partDTO);
        Task<PartDTO?> UpdatePart(int id, UpdatePartDTO partDTO);
        Task<bool> DeletePart(int id);
    }
}
