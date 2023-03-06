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
            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<TruckDto, Truck>();
                config.CreateMap<Truck, TruckDto>();
            }
            );

            return mappingConfiguration;
        }
    }
}
