namespace TicketToCode.Api.Endpoints.Favorites;

public class GetAllFavorites : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/Favorites", Handle)
        .WithTags("Favorites")
        .WithSummary("Get all your favorite recipes");

    // DTO's
    public record Response(
        int Id,
        string Name,
        string CategoryDescription);

    // Logic
    private static List<Response> Handle(IDatabase db, int userId)
    {
        // Get the list of favorite recipe IDs for the current user
        var favoriteRecipeIds = db.Favorites
            .Where(f => f.UserId == userId)
            .Select(f => f.RecipeId)
            .ToList();

        // Retrieve the recipes that are in the user's favorites
        return db.Recipes
            .Where(recipe => favoriteRecipeIds.Contains(recipe.Id))
            .Select(item => new Response(
                item.Id,
                item.Name,
                EnumHelper.GetEnumDescription(item.Category)))
            .ToList();
    }
}