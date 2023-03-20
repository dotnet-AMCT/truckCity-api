using System.ComponentModel.DataAnnotations;

namespace truckCity_api.Models.Dto
{
    public record PlantDto(Guid Id,
                           string Name,
                           uint MaxCapacity,
                           uint CurrentCapacity,
                           string Address);
    public record CreatePlantDto([Required] string Name,
                                 [Required] uint MaxCapacity,
                                 [Required] uint CurrentCapacity,
                                 [Required] string Address);
    public record UpdatePlantDto(string? Name,
                                 uint? MaxCapacity,
                                 uint? CurrentCapacity,
                                 string? Address);
}
