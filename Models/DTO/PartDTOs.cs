using System.ComponentModel.DataAnnotations;

namespace truckCity_api.Models.DTO
{
    public record PartDTO(Guid Id,
                          string Name,
                          string Code,
                          Guid? TruckId);
    public record CreatePartDTO([Required] string Name,
                                [Required] string Code,
                                Guid? TruckId);
    public record UpdatePartDTO(string? Name,
                                string? Code);
    public record ReplacementPartDTO(Guid Id,
                                     string Name,
                                     string Code);
    public record PartStock(string Name,
                            int StockQuantity);
}
