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

        var orderedTags = new[] { "Products", "Keyboards", "Mice", "Mousepads" };
        swaggerDoc.Tags = swaggerDoc.Tags?
            .OrderBy(tag =>
            {
                int index = Array.IndexOf(orderedTags, tag.Name);
                return index == -1 ? int.MaxValue : index; // if the tag isn't known, move it to the end. 
            })
            .ThenBy(tag => tag.Name)
            .ToList();
    }
}