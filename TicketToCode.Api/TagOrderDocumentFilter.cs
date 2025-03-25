using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TicketToCode.Api;

public class TagOrderDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (swaggerDoc != null || !swaggerDoc.Tags.Any())
        {

            swaggerDoc.Tags = context.ApiDescriptions
                .SelectMany(desc => desc.ActionDescriptor.EndpointMetadata.OfType<TagsAttribute>())
                .SelectMany(attr => attr.Tags)
                .Distinct()
                .Select(tag => new OpenApiTag { Name = tag })
                .ToList();
        }
    }
}