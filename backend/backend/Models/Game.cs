namespace backend.Models
{
    public class Game
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }

        public ICollection<UserGameLibrary> UserGameLibraries = new List<UserGameLibrary>();
    }
}
