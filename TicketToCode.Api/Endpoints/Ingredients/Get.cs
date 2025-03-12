namespace TicketToCode.Api.Endpoints.Ingredients;

public class GetIngredient : IEndpoint
{

    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/Ingredients/{id}", Handle)
        .WithTags("Ingredients")
        .WithSummary("Get single Ingredient");

    // DTO's
    public record Request(
        int Id);
    public record Response(int Id, string Name, IngredientType Type);

    // Logic
    private static Results<Ok<Response>, BadRequest<string>> Handle([AsParameters]Request request, IDatabase db)
    {
        var ingredient = db.Ingredients.Where(x => x.Id == request.Id).FirstOrDefault();
        if (ingredient is null)
        {
            return TypedResults.BadRequest("There's no ingredient with that Id");
        }
        var response = new Response(ingredient.Id, ingredient.Name, ingredient.Type);
        return TypedResults.Ok(response);

    }
}