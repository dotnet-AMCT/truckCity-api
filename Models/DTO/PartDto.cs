using System.ComponentModel.DataAnnotations;

namespace truckCity_api.Models.Dto
{
    public record PartDto(Guid Id,
                          string Name,
                          string Code,
                          Guid? TruckId);
    public record CreatePartDto([Required] string Name,
                                [Required] string Code,
                                Guid? TruckId);
    public record UpdatePartDto(string? Name,
                                string? Code);
    public record ReplacementPartDto(Guid Id,
                                     string Name,
                                     string Code);
    public record PartStock(string Name,
                            int StockQuantity);
}
