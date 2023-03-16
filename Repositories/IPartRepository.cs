using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using truckCity_api.Models.Dto;


namespace truckCity_api.Repositories
{
    public interface IPartRepository
    {
        Task<IEnumerable<PartDto>> GetPart();
        Task<PartDto> GetPart(Guid id);
        Task<PartDto?> CreatePart(CreatePartDto createPartDto);
        Task<PartDto?> UpdatePart(Guid id, UpdatePartDto updatePartDto);
        Task<bool> DeletePart(Guid id);
    }
}
