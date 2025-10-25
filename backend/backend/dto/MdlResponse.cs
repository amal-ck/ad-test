namespace backend.dto
{
    public class MdlResponse
    {
        public bool Success { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string ErrorMsg { get; set; }
        public string ErrorExMsg { get; set; }
    }
}
