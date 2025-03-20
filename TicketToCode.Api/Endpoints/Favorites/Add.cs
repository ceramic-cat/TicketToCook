namespace TicketToCode.Api.Endpoints.Favorites
{
    public class Add : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapPost("/Favorites", Handle)
            .WithTags("Favorites")
            .WithSummary("Add a recipe to the favorites list");

        // DTO for the request
        public record Request(int UserId, int RecipeId);

            
        // Logic to handle adding a favorite
        private static Results<Ok<string>, BadRequest<string>> Handle([FromBody] Request request, IDatabase db)
        {
            // Check if the favorite already exists
            var existingFavorite = db.Favorites.FirstOrDefault(f => f.UserId == request.UserId && f.RecipeId == request.RecipeId);
            if (existingFavorite != null)
            {
                return TypedResults.BadRequest("This recipe is already in your favorites.");
            }

            // Create a new favorite entry
            var newFavorite = new Favorite(request.UserId, request.RecipeId);
            db.Favorites.Add(newFavorite);

            return TypedResults.Ok("Recipe added to favorites successfully.");
        }
    }
}