namespace truckCity_api.Models.Dto
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;

        public object? Result { get; set; } = null;

        public string DisplayMessage { get; set; } = string.Empty;

        public List<string> ErrorsMessages { get; set; } = new List<string>();
    }
}
