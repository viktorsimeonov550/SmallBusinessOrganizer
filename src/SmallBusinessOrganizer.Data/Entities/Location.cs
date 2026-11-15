namespace SmallBusinessOrganizer.Data.Entities;

public class Location : BaseEntity
{
    public int WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }
    public string Code { get; set; } = string.Empty;

    public ICollection<StockLevel> StockLevels { get; set; } = new List<StockLevel>();
}
