namespace TicketToCode.Api.Endpoints.Recipes;

public class UpdateRecipe : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPatch("/Recipes/Update/{id}", Handle)
        .WithTags("Recipes")
        .WithSummary("Update an Recipe");

    // DTOs
    public record Request(
        int Id,
        string? Name,
        string? Description,
        List<RecipeIngredient>? Ingredients,
        string? Instructions,
        Category? Category);
    public record Response(
        int Id,
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

        // patch the item
        recipe.Name = request.Name ?? recipe.Name;
        recipe.Description = request.Description ?? recipe.Description;
        recipe.Ingredients = request.Ingredients ?? recipe.Ingredients;
        recipe.Instructions = request.Instructions ?? recipe.Instructions;
        recipe.Category = request.Category ?? recipe.Category;

        var ingredients = recipe.Ingredients.Select(ri => new RecipeIngredientResponse(
            ri.Ingredient.Name,
            EnumUtilities.GetEnumDescription(ri.Ingredient.Type),
            ri.Quantity,
            EnumUtilities.GetEnumDescription(ri.Ingredient.Unit)
        )).ToList();

        // Create response DTO
        var response = new Response(
            recipe.Id,
            recipe.Name,
            recipe.Description,
            ingredients,
            recipe.Instructions,
            EnumUtilities.GetEnumDescription(recipe.Category));

        return TypedResults.Ok(response);
    }
}
