namespace SmallBusinessOrganizer.Data.Entities;

public class SalesOrder : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public SalesOrderStatus Status { get; set; } = SalesOrderStatus.New;
    public DateTime CreatedAt { get; set; }

    public ICollection<SalesOrderItem> Items { get; set; } = new List<SalesOrderItem>();
}
