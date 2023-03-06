namespace truckCity_api.Models.Dto
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }

        public string DisplayMessage { get; set; }

        public List<string> ErrorsMessages { get; set; }

        public ResponseDto (bool isSuccess, object result, string displayMessage, List<string> errorsMessages)
        {
            IsSuccess = isSuccess;
            Result = result;
            DisplayMessage = displayMessage;
            ErrorsMessages = errorsMessages;
        }
    }
}
