namespace ApplicationCore.Entities;

public class UserRole
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    // navigation fields
    public User User { get; set; }
    public Role Role { get; set; }
}