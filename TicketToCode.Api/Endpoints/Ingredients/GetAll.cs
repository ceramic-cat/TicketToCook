namespace TicketToCode.Api.Endpoints.Ingredients;

public class GetAll : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
    .MapGet("/Ingredients", Handle)
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

