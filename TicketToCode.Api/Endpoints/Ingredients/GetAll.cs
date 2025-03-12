namespace TicketToCode.Api.Endpoints.Ingredients;

public class GetAllIngredients : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
    .MapGet("/Ingredients", Handle)
    .WithTags("Ingredients")
    .WithSummary("Get all Ingredients");

    // DTO's

    public record Response(int Id, string Name, IngredientType Type);

    // Logic
    private static List<Response> Handle(IDatabase db)
    {
        return db.Ingredients
            .Select(item => new Response(
                item.Id,
                item.Name,
                item.Type))
            .ToList();

    }


    }

