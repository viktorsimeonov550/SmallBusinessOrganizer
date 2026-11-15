namespace SmallBusinessOrganizer.Data.Entities;

public class StockMovement : BaseEntity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int? FromLocationId { get; set; }
    public Location? FromLocation { get; set; }
    public int? ToLocationId { get; set; }
    public Location? ToLocation { get; set; }
    public int Quantity { get; set; }
    public StockMovementType Type { get; set; }
    public string Reason { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }
}
