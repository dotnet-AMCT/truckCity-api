using System.ComponentModel.DataAnnotations;

namespace truckCity_api.Models.DTO
{
    public record PartDTO(Guid Id,
                          string Name,
                          string Code,
                          string TruckId);

    public record CreatePartDTO([Required] string Name,
                                [Required] string Code,
                                [Required] int TruckId);
    public record UpdatePartDTO(string? Name,
                                string? Code,
                                int? TruckId);
}
