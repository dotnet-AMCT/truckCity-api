using System.Drawing.Drawing2D;
using truckCity_api.Models;
using truckCity_api.Models.Dto;

namespace truckCity_api.Utilities;

public static class TruckExtension
{
    public static TruckDto AsDto(this Truck truck)
    {
        if (truck.Plant == null)
        {
            return new TruckDto(truck.Id, truck.LicencePlate, truck.Brand,
                            truck.Model, truck.Year, truck.Kilometres,
                            null, truck.IsSold, truck.BrokenParts,
                            truck.CompatiblePartCodes);
        }
        else
        {
            return new TruckDto(truck.Id, truck.LicencePlate, truck.Brand,
                truck.Model, truck.Year, truck.Kilometres,
                truck.Plant.Id, truck.IsSold, truck.BrokenParts,
                truck.CompatiblePartCodes);
        }
    }
}
