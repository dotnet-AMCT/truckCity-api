namespace truckCity_api.Models.DTO
{
    public record ReplacementPartsRequestDTO(List<string> partNames,
                                             List<string> partCodes);
}
