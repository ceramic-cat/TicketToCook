using System.Security.Claims;

namespace TicketToCode.Api.Endpoints.Auth;

public class GetUserProfile : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/auth/fetch", Handle)
        .WithSummary("Get current user profile information")
        .WithTags("User")
        .RequireAuthorization();



    // Models
    public record Response(string Username, string Role, string Message);

    // Logic
    private static IResult Handle(ClaimsPrincipal user)
    {
        var username = user.FindFirstValue(ClaimTypes.Name);
        var role = user.FindFirstValue(ClaimTypes.Role);

        if (string.IsNullOrEmpty(username))
        {
            return TypedResults.Unauthorized();
        }

        var response = new Response(
            username,
            role,
            $"Hello {username}! You have the role {role ?? "User"}."
            );

        return TypedResults.Ok(response);
    }

}