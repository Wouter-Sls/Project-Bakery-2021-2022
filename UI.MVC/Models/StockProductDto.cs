namespace ProjectBakeryUI.MVC.Models
{
    public class StockProductDto
    {
        public double TotalStock { get; }
        public double TotalPrice { get; }
        public long BakeryId { get; }
        public long PastryId { get; }

        public StockProductDto(double totalStock, double totalPrice, long bakeryId, long pastryId)
        {
            TotalStock = totalStock;
            TotalPrice = totalPrice;
            BakeryId = bakeryId;
            PastryId = pastryId;
        }
    }
}