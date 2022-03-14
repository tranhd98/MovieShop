namespace MovieShopMVC.Services;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    int userId { get; }
    string Email { get; }
    string FirstName { get; }
    string LastName { get; }
    bool IsAdmin { get; }
    List<string> Roles { get; }
    string IpAddress { get; }
}