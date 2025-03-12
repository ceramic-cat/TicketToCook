using TicketToCode.Core.Models;

namespace TicketToCode.Core.Data;

public interface IDatabase
{
    List<User> Users { get; set; }
    List<Ingredient> Ingredients { get; set; }
}

public class Database : IDatabase
{
    public List<User> Users { get; set; } = new List<User>();
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
