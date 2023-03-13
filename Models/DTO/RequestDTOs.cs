namespace truckCity_api.Models.DTO
{
    public record ReplacementPartsRequestDTO(Guid truckId,
                                             List<string> partCodes);
}
