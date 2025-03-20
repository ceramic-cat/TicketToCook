namespace TicketToCode.Core.Models;

public class Favorite
{
    public int UserId { get; set; }
    public int RecipeId { get; set; }

    public Favorite(int userId, int recipeId)
    {
        UserId = userId;
        RecipeId = recipeId;
    }
}
