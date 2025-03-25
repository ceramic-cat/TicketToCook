using System.Security.Claims;

namespace TicketToCode.Api.Endpoints.Favorites;

public class IsRecipeFavorite : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/user/favorites/check/{id}", Handle)
        .WithSummary("Check if a specific recipe is in user's favorites")
        .WithTags("Favorite")
        .RequireAuthorization();

    // Models
    public record Response(bool IsFavorite);

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

        // Get user
        var currentUser = database.Users.FirstOrDefault(u => u.Username == username);
        if (currentUser == null)
        {
            return TypedResults.NotFound($"User {username} not found");
        }

        // Check if recipe is in favorites
        bool isFavorite = currentUser.Favorites.Any(r => r.Id == id);

        return TypedResults.Ok(new Response(isFavorite));
    }
}