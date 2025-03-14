namespace TicketToCode.Core.Models;

public class Favorite
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Favorite(int recipeId)
    {
        RecipeId = recipeId;
    }
}