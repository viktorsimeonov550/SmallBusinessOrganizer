namespace SmallBusinessOrganizer.Data.Entities;

public class Warehouse : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public ICollection<Location> Locations { get; set; } = new List<Location>();
}
