namespace TicketToCode.Api.Endpoints.Ingredients;

public class UpdateIngredient : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPatch("/Ingredients/Update/{id}", Handle)
        .WithTags("Ingredients")
        .WithSummary("Update an Ingredient");

    // DTOs
    public record Request(
        int Id,
        string? Name,
        IngredientType? Type,
        MeasurementUnit? Unit);
    public record Response(
        int Id,
        string Name,
        string TypeDescription,
        string UnitDescription);

    // Logic
    private static Results<Ok<Response>, BadRequest<string>> Handle([AsParameters] Request request, IDatabase db)
    {
        var ingredient = db.Ingredients.Where(x => x.Id == request.Id).FirstOrDefault();
        if (ingredient is null)
        {
            return TypedResults.BadRequest("There's no ingredient with that Id");
        }

        // patch the item
        ingredient.Name = request.Name ?? ingredient.Name;
        ingredient.Type = request.Type ?? ingredient.Type;
        ingredient.Unit = request.Unit ?? ingredient.Unit;

        // Create response DTO
        var response = new Response(
            ingredient.Id,
            ingredient.Name,
            EnumUtilities.GetEnumDescription(ingredient.Type),
            EnumUtilities.GetEnumDescription(ingredient.Unit));

        return TypedResults.Ok(response);
    }
}
