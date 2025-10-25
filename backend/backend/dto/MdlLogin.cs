namespace backend.dto
{
    public class DtoLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class DtoRegister
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
