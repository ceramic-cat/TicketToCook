namespace TicketToCode.Api.Endpoints.Favorites
{
    public class GetFavorite : IEndpoint
    {

        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapGet("/Favorites/{id}", Handle)
            .WithTags("Favorites")
            .WithSummary("Get one of your favorite recipes");

        // DTOs
        public record Request(
            int Id);
        public record Response(
            string Name,
            string Description,
            List<RecipeIngredient> Ingredients,
            string Instructions,
            Category Category);

        // Logic
        private static Results<Ok<Response>, BadRequest<string>> Handle([AsParameters] Request request, IDatabase db, int userId)
        {
            var favorite = db.Favorites.FirstOrDefault(f => f.RecipeId == request.Id && f.UserId == userId);
            if (favorite is null)
            {
                return TypedResults.BadRequest("This recipe is not in your favorites.");
            }

            var recipe = db.Recipes.FirstOrDefault(x => x.Id == request.Id);
            if (recipe is null)
            {
                return TypedResults.BadRequest("There's no recipe with that Id");
            }

            var response = new Response(
                recipe.Name,
                recipe.Description,
                recipe.Ingredients,
                recipe.Instructions,
                recipe.Category);
            return TypedResults.Ok(response);
        }

    }
    
}
