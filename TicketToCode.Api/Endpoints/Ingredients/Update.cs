using System.Reflection.Metadata;

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
        string Name,
        IngredientType Type);

    public record Response(
        int Id, 
        string Name, 
        IngredientType Type);

    // Logic
    private static Results<Ok<Response>, BadRequest<string>> Handle([AsParameters] Request request, IDatabase db)
    {
        var ingredient = db.Ingredients.Where(x => x.Id == request.Id).FirstOrDefault();
        if (ingredient is null)
        {
            return TypedResults.BadRequest("There's no ingredient with that Id");
        }

        // Patch the item
        ingredient.Name = request.Name;
        ingredient.Type = request.Type;

        var response = new Response(ingredient.Id, ingredient.Name, ingredient.Type);
        return TypedResults.Ok(response);
    }
}
