namespace SmallBusinessOrganizer.Data.Entities;

public class Product : BaseEntity
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public string Unit { get; set; } = "бр.";
    public int MinStockLevel { get; set; }
    public decimal UnitPrice { get; set; }

    public ICollection<StockLevel> StockLevels { get; set; } = new List<StockLevel>();
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
    public ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();
}
