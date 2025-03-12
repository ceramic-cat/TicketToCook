using System.Reflection.Metadata;

namespace TicketToCode.Api.Endpoints.Ingredients;

public class UpdateIngredient
    // MapEndpoints doesn't seem to find this for some reason.
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
    .MapPut("/Ingredients/{id}", Handle)
    .WithTags("Ingredients")
    .WithSummary("Update an Ingredient");

    // DTO's
    public record Request(
        int Id,
        string Name,
        IngredientType Type);
    public record Response(
        int Id, 
        string Name, 
        IngredientType Type);

    // Logic
    private static Results<Ok<Response>, BadRequest<string>> Handle([AsParameters]Request request, IDatabase db)
    {
        var ingredient = db.Ingredients.Where(x => x.Id == request.Id).FirstOrDefault();
        if (ingredient is null)
        {
            return TypedResults.BadRequest("There's no ingredient with that Id");
        }

        // patch the item
        ingredient.Name = request.Name;
        ingredient.Type = request.Type;

        var response = new Response(ingredient.Id, ingredient.Name, ingredient.Type);
        return TypedResults.Ok(response);

    }


}
