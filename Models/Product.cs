namespace AutoService.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string SKU { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public double StockQuantity { get; set; } = 0;
        public string UM { get; set; } = "buc";
        public decimal AveragePurchasePrice { get; set; } = 0;
        public int MinStockAlert { get; set; } = 2;
    }
}