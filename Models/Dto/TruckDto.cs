using System.ComponentModel.DataAnnotations;

namespace truckCity_api.Models.Dto
{
    public record TruckDto (Guid Id,
                            string LicencePlate,
                            string Brand,
                            string Model,
                            uint Year,
                            uint Kilometres,
                            Guid? PlantId,
                            bool IsSold,
                            List<string>? BrokenParts,
                            List<string>? CompatiblePartCodes);

    public record CreateTruckDto ([Required] string LicencePlate,
                                  [Required] string Brand,
                                  [Required] string Model,
                                  [Required] uint Year,
                                  [Required] uint Kilometres,
                                  [Required] bool IsSold,
                                  Guid? PlantId = null,
                                  List<string>? BrokenParts = null,
                                  List<string>? CompatiblePartCodes = null);
    public record UpdateTruckDto (string? Brand = null,
                                  string? Model = null,
                                  uint? Year = null,
                                  uint? Kilometres = null,
                                  Guid? PlantId = null,
                                  bool? IsSold = null,
                                  List<string>? BrokenParts = null,
                                  List<string>? CompatiblePartCodes = null);

}
