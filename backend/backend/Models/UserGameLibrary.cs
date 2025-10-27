namespace backend.Models
{
    public class UserGameLibrary
    {
        public DateTime PurchasedOn { get; set; }
        public string? TotalPlaytime { get; set; }
        public decimal priceAtPurchase { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public int userId { get; set; }
        public User User { get; set; }
    }
}
