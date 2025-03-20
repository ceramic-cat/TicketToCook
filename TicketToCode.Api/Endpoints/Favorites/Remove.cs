namespace TicketToCode.Api.Endpoints.Favorites
{
    public class Remove : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapDelete("/Favorites/{recipeId}", Handle)
            .WithTags("Favorites")
            .WithSummary("Remove a recipe from favorites");

        // DTO for request
        public record Request(int RecipeId);

        // Logic to handle the removal of a favorite recipe
        private static Results<Ok<string>, NotFound<string>> Handle([AsParameters] Request request, IDatabase db, int userId)
        {
            // Check if the favorite exists
            var favorite = db.Favorites.FirstOrDefault(f => f.RecipeId == request.RecipeId && f.UserId == userId);
            if (favorite is null)
            {
                return TypedResults.NotFound("This recipe is not in your favorites.");
            }

            // Remove the favorite from the list
            db.Favorites.Remove(favorite);
            return TypedResults.Ok("Recipe removed from favorites successfully.");
        }
    }
}