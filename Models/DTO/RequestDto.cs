namespace truckCity_api.Models.Dto
{
    public record ReplacementPartsRequestDto(Guid truckId,
                                             List<string> partCodes);
}
