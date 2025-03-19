using System.Diagnostics;

namespace TicketToCode.Api.Endpoints.Recipes;

public class CreateRecipes : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/Recipes", Handle)
        .WithTags("Recipes")
        .WithSummary("Create Recipe");

    // DTO's
    public record Request(
        string Name,
        string Description,
        List<RecipeIngredient> Ingredients,
        string Instructions,
        Category Category
        );
    public record Response(int Id, string Name, string CategoryDescription);

    // Logic
    private static Results<Ok<Response>, BadRequest<string>> Handle(Request request, IDatabase db)
    {
        var recipeExists = db.Recipes.Any(x => x.Name == request.Name);
        if (recipeExists)
        {
            return TypedResults.BadRequest("Recipe of that name already exists");
        }

        var newRecipe = new Recipe
        {
            Name = request.Name,
            Description = request.Description,
            Ingredients = request.Ingredients,
            Instructions = request.Instructions,
            Category = request.Category
        };

        // Assign Id
        newRecipe.Id = db.Recipes.Count() > 0 ? db.Recipes.Max(x => x.Id + 1) : 1;

        db.Recipes.Add(newRecipe);
        
        // Return response dto
        var response = new Response(newRecipe.Id, newRecipe.Name, EnumHelper.GetEnumDescription(newRecipe.Category));
        return TypedResults.Ok(response);
    }
}