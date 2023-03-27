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
                                  [Required, MaxLength(100), MinLength(1)] string Brand,
                                  [Required, MaxLength(100), MinLength(1)] string Model,
                                  [Required, Range(2000, 2100)] uint Year,
                                  [Required] uint Kilometres,
                                  [Required] bool IsSold);

    public record UpdateTruckDto ([MaxLength(100), MinLength(1)] string? Brand = null,
                                  [MaxLength(100), MinLength(1)] string? Model = null,
                                  [Range(2000,2100)] uint? Year = null,
                                  uint? Kilometres = null,
                                  bool? IsSold = null);
}
