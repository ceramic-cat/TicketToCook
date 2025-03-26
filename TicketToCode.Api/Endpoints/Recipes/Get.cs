namespace TicketToCode.Api.Endpoints.Recipes;

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
        List<RecipeIngredientResponse> Ingredients,
        string Instructions,
        string CategoryDescription);

    public record RecipeIngredientResponse(
        string IngredientName,
        string IngredientType,
        double Quantity,
        string MeasurementUnit);

    // Logic
    private static Results<Ok<Response>, BadRequest<string>> Handle([AsParameters] Request request, IDatabase db)
    {
        var recipe = db.Recipes.Where(x => x.Id == request.Id).FirstOrDefault();
        if (recipe is null)
        {
            return TypedResults.BadRequest("There's no recipe with that Id");
        }

        var ingredients = recipe.Ingredients.Select(ri => new RecipeIngredientResponse(
            ri.Ingredient.Name,
            EnumUtilities.GetEnumDescription(ri.Ingredient.Type),
            ri.Quantity,
            EnumUtilities.GetEnumDescription(ri.Ingredient.Unit)
        )).ToList();

        var response = new Response(
            recipe.Name,
            recipe.Description,
            ingredients,
            recipe.Instructions,
            EnumUtilities.GetEnumDescription(recipe.Category));
        return TypedResults.Ok(response);

    }
}