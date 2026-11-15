namespace SmallBusinessOrganizer.Data.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
}
