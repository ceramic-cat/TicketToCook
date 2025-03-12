using System.Diagnostics;

namespace TicketToCode.Api.Endpoints.Ingredients;

public class CreateIngredient : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/Ingredients", Handle)
        .WithSummary("Create Ingredient");

    // DTO's
    public record Request(
        string Name,
        IngredientType Type);
    public record Response(int Id, string Name);

    // Logic
    private static Results<Ok<Response>, BadRequest<string>> Handle(Request request, IDatabase db)
    {
        var ingredientExists = db.Ingredients.Any(x => x.Name == request.Name);
        if (ingredientExists)
        {
            return TypedResults.BadRequest("Ingredient of that name already exists");
        }

        // Assign Id
        var ingredient = new Ingredient(request.Name, request.Type);
        ingredient.Id = db.Ingredients.Count() > 0 ? db.Ingredients.Max(x => x.Id + 1) : 1;

        db.Ingredients.Add(ingredient);
        // Return response dto
        var response = new Response(ingredient.Id, ingredient.Name);
        return TypedResults.Ok(response);

    }
}
