using System.Security.Claims;

namespace TicketToCode.Api.Endpoints.Favorites;

public class GetUserFavorites : IEndpoint

{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/user/favorites", Handle)
        .WithSummary("Get current user's favorite recipes")
        .WithTags("Favorites")
        .RequireAuthorization();

    // Models - Updated to match your Recipe model
    public record RecipeDto(int Id, string Name, string Description, Category Category);
    public record Response(List<RecipeDto> Favorites);

    // Logic
    private static IResult Handle(
        ClaimsPrincipal user,
        IDatabase database)
    {
        var username = user.FindFirstValue(ClaimTypes.Name);
        if (string.IsNullOrEmpty(username))
        {
            return TypedResults.Unauthorized();
        }

        // Get user with favorites
        var currentUser = database.Users.FirstOrDefault(u => u.Username == username);
        if (currentUser == null)
        {
            return TypedResults.NotFound($"User {username} not found");
        }

        // Map recipes to DTOs - adjusted for your Recipe model
        var recipeDtos = currentUser.Favorites.Select(r =>
            new RecipeDto(
                r.Id,
                r.Name,
                r.Description,
                r.Category
            )).ToList();

        return TypedResults.Ok(new Response(recipeDtos));
    }
}

