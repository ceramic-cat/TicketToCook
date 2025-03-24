namespace TicketToCode.Core.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } = UserRoles.User; // Default role
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Recipe> Favorites { get; set; }

    public User(string name, string pwd)
    {
        Username = name;
        PasswordHash = pwd;
        Favorites = new List<Recipe>();
    }

    public User(string name, string pwd, string role)
    {
        Username = name;
        PasswordHash = pwd;
        Favorites = new List<Recipe>();

        if (role == UserRoles.Admin)
        {
            Role = role;
        }
    }

    public void SetAdminRole()
    {
        this.Role = UserRoles.Admin;
    }
    public void SetUserRole()
    {
        this.Role = UserRoles.User;
    }

}

// Static class to define roles
public static class UserRoles
{
    public const string Admin = "Admin";
    public const string User = "User";

}