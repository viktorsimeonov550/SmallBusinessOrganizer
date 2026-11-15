namespace SmallBusinessOrganizer.Data.Entities;

public class PurchaseOrder : BaseEntity
{
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.New;
    public DateTime CreatedAt { get; set; }

    public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
}
