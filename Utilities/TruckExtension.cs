using System.Drawing.Drawing2D;
using truckCity_api.Models;
using truckCity_api.Models.Dto;

namespace truckCity_api.Utilities;

public static class TruckExtension
{
    public static TruckDto AsDto(this Truck truck)
    {
        return new TruckDto(truck.Id, truck.LicencePlate, truck.Brand,
                            truck.Model, truck.Year, truck.Kilometres,
                            truck.PlantId, truck.IsSold, truck.BrokenParts,
                            truck.CompatiblePartCodes);
    }
}
