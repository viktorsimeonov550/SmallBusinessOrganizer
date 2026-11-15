namespace SmallBusinessOrganizer.Data.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Viewer;
    public bool IsActive { get; set; } = true;

    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
