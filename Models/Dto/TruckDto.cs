using System.ComponentModel.DataAnnotations;

namespace truckCity_api.Models.Dto
{
    public record TruckDto (Guid Id,
                            string LicencePlate,
                            string Brand,
                            string Model,
                            uint Year,
                            uint Kilometres,
                            bool IsSold,
                            Guid? PlantId,
                            List<string>? BrokenParts,
                            List<string>? CompatiblePartCodes);

    public record CreateTruckDto ([Required] string LicencePlate,
                                  [Required] string Brand,
                                  [Required] string Model,
                                  [Required] uint Year,
                                  [Required] uint Kilometres,
                                  [Required] bool IsSold);

    public record UpdateTruckDto (string? Brand = null,
                                  string? Model = null,
                                  uint? Year = null,
                                  uint? Kilometres = null,
                                  bool? IsSold = null);
}
