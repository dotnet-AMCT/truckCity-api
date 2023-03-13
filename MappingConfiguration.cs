using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using truckCity_api.Models;
using truckCity_api.Models.DTO;


namespace truckCity_api
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfiguration = new MapperConfiguration( config =>
            {
                config.CreateMap<PartDTO, Part>();
                config.CreateMap<Part, PartDTO>();
                config.CreateMap<CreatePartDTO, Part>();
                config.CreateMap<Part, CreatePartDTO>();
                config.CreateMap<UpdatePartDTO, Part>();
                config.CreateMap<Part, UpdatePartDTO>();
                config.CreateMap<Part, ReplacementPartDTO>();
            }
            );

            return mappingConfiguration;
        }
    }
}
