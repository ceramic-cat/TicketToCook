namespace TicketToCode.Core.Models;

public class ShoppingList
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<Ingredient> Items { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ShoppingList(int userId)
    {
        UserId = userId;
        Items = new List<Ingredient>();
    }
}