﻿using truckCity_api.Models;
using truckCity_api.Models.Dto;

namespace truckCity_api.Utilities;

public static class PlantExtension
{
    public static PlantDto AsDto(this Plant plant)
    {
        if (plant.Trucks == null)
        {
            return new PlantDto(plant.Id, plant.Name, plant.MaxCapacity,
                            plant.CurrentCapacity, plant.Address);
        }
        else
        {
            return new PlantDto(plant.Id, plant.Name, plant.MaxCapacity,
                            plant.CurrentCapacity, plant.Address);
        }
    }
}
