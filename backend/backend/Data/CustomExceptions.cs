namespace backend.Data
{
    public class CustomExceptions
    {
    }

    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }

}
