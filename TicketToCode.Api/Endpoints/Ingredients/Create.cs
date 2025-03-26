namespace TicketToCode.Api.Endpoints.Ingredients;

public class CreateIngredient : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/Ingredients", Handle)
        .WithTags("Ingredients")
        .WithSummary("Create Ingredient");

    // DTOs
    public record Request(string Name, IngredientType Type, MeasurementUnit Unit);
    public record Response(int Id, string Name, string TypeDescription, string UnitDescription);

    // Logic
    private static Results<Ok<Response>, BadRequest<string>> Handle(Request request, IDatabase db)
    {
        var ingredientExists = db.Ingredients.Any(x => x.Name == request.Name);
        if (ingredientExists)
        {
            return TypedResults.BadRequest("Ingredient of that name already exists");
        }

        // Assign Id
        var ingredient = new Ingredient(request.Name, request.Type, request.Unit);
        ingredient.Id = db.Ingredients.Count() > 0 ? db.Ingredients.Max(x => x.Id + 1) : 1;

        db.Ingredients.Add(ingredient);
        // Return response dto
        var response = new Response(
            ingredient.Id,
            ingredient.Name,
            EnumUtilities.GetEnumDescription(ingredient.Type),
            EnumUtilities.GetEnumDescription(ingredient.Unit));
        return TypedResults.Ok(response);
    }
}
