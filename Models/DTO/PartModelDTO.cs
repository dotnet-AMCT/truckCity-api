using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace truckCity_api.Models.DTO
{
    public class PartModelDTO
    {
        public int Id { get; }

        public PartNames Name { get; }

        public int CodeId { get; }

        public PartCode Code { get; } = null!;

        public int? TruckId { get; }

        public Truck? Truck { get; }

        public PartModelDTO(
            int id, 
            PartNames name, 
            int codeId, 
            int? truckId)
        {
            Id = id;
            Name = name;
            CodeId = codeId;
            TruckId = truckId;
        }
    }
}
