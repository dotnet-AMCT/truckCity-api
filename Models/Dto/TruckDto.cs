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
                                  Guid? PlantId,
                                  [Required] bool IsSold,
                                  List<string>? BrokenParts,
                                  List<string>? CompatiblePartCodes);
    public record UpdateTruckDto (string? Brand,
                                  string? Model,
                                  uint? Year,
                                  uint? Kilometres,
                                  Guid? PlantId,
                                  bool? IsSold,
                                  List<string>? BrokenParts,
                                  List<string>? CompatiblePartCodes);

}
