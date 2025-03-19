namespace TicketToCode.Api.Endpoints.Ingredients;

public class GetRecipe : IEndpoint
{

    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/Recipes/{id}", Handle)
        .WithTags("Recipes")
        .WithSummary("Get single Recipe");

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
    private static Results<Ok<Response>, BadRequest<string>> Handle([AsParameters]Request request, IDatabase db)
    {
        var recipe = db.Recipes.Where(x => x.Id == request.Id).FirstOrDefault();
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