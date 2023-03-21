using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using truckCity_api.Models;
using truckCity_api.Models.Dto;


namespace truckCity_api
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfiguration = new MapperConfiguration( config =>
            {
                config.CreateMap<PartDto, Part>();
                config.CreateMap<Part, PartDto>();
                config.CreateMap<CreatePartDto, Part>();
                config.CreateMap<Part, CreatePartDto>();
                config.CreateMap<UpdatePartDto, Part>();
                config.CreateMap<Part, UpdatePartDto>();
                config.CreateMap<Part, ReplacementPartDto>();
            }
            );

            return mappingConfiguration;
        }
    }
}
