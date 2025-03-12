using System.Reflection.Metadata;

namespace TicketToCode.Api.Endpoints.Ingredients;

public class UpdateRecipe
// MapEndpoints doesn't seem to find this for some reason.
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
    .MapPatch("/Recipes/Update/{id}", Handle)
    .WithSummary("Update an Recipe");

    // DTO's
    public record Request(
        int Id,
        string Name,
        string Description,
        List<Ingredient> Ingredients,
        string Instructions,
        Category Category);
    public record Response(
        int Id,
        string Name,
        string Description,
        List<Ingredient> Ingredients,
        string Instructions,
        Category Category);

    // Logic
    private static Results<Ok<Response>, BadRequest<string>> Handle([AsParameters] Request request, IDatabase db)
    {
        var recipe = db.Recipes.Where(x => x.Id == request.Id).FirstOrDefault();
        if (recipe is null)
        {
            return TypedResults.BadRequest("There's no recipe with that Id");
        }

        // patch the item
        recipe.Name = request.Name;
        recipe.Description = request.Description;
        recipe.Ingredients = request.Ingredients;
        recipe.Instructions = request.Instructions;
        recipe.Category = request.Category;

        // Create response DTO
        var response = new Response(
            recipe.Id, 
            recipe.Name, 
            recipe.Description,
            recipe.Ingredients,
            recipe.Instructions,
            recipe.Category);

        return TypedResults.Ok(response);

    }


}
