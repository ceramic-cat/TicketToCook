using System.Security.Claims;

namespace TicketToCode.Api.Endpoints.Favorites;
public class ToggleFavoriteRecipe : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/user/favorites/toggle/{id}", Handle)
        .WithSummary("Add or remove a recipe from user's favorites")
        .WithTags("Favorites")
        .RequireAuthorization();

    // Models
    public record Response(bool IsFavorite, string Message);

    // Logic
    private static IResult Handle(
        int id,
        ClaimsPrincipal user,
        IDatabase database)
    {
        var username = user.FindFirstValue(ClaimTypes.Name);
        if (string.IsNullOrEmpty(username))
        {
            return TypedResults.Unauthorized();
        }

        // Get user and recipe
        var currentUser = database.Users.FirstOrDefault(u => u.Username == username);
        if (currentUser == null)
        {
            return TypedResults.NotFound($"User {username} not found");
        }

        var recipe = database.Recipes.FirstOrDefault(r => r.Id == id);
        if (recipe == null)
        {
            return TypedResults.NotFound($"Recipe with ID {id} not found");
        }

        // Check if recipe is already a favorite
        var existingFavorite = currentUser.Favorites.FirstOrDefault(r => r.Id == id);

        if (existingFavorite != null)
        {
            // Remove from favorites
            currentUser.Favorites.Remove(existingFavorite);
            return TypedResults.Ok(new Response(false, $"Recipe '{recipe.Name}' removed from favorites"));
        }
        else
        {
            // Add to favorites
            currentUser.Favorites.Add(recipe);
            return TypedResults.Ok(new Response(true, $"Recipe '{recipe.Name}' added to favorites"));
        }
    }
}