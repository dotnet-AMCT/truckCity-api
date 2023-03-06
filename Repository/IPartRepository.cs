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

        Task<PartDTO> PostPutPart(PartDTO partDTO);

        Task<bool> DeletePart(int id);
    }
}
