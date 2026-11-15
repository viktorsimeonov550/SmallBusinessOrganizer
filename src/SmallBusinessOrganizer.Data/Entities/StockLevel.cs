namespace SmallBusinessOrganizer.Data.Entities;

// Current quantity per product and location. Composite key: (ProductId, LocationId).
public class StockLevel
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int LocationId { get; set; }
    public Location? Location { get; set; }
    public int Quantity { get; set; }
}
