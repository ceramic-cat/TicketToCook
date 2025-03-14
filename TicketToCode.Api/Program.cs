using Microsoft.OpenApi.Models;
using TicketToCode.Api;
using TicketToCode.Api.Endpoints;
using TicketToCode.Api.Services;
using TicketToCode.Core.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Default mapping is /openapi/v1.json
builder.Services.AddOpenApi();
 
builder.Services.AddSingleton<IDatabase, Database>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add cookie authentication
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

builder.Services.AddAuthorization();



// To allow cookies CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7044") // Change this to match your Blazor app URL
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});

// Add Swaggerdoc options and sorting by tags 
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TicketToCook",
        Version = "v1",
        Description = "A API for an recipe database"
    });
    options.InferSecuritySchemes();
    options.DocumentFilter<TagOrderDocumentFilter>();

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Todo: consider scalar? https://youtu.be/Tx49o-5tkis?feature=shared
    app.UseSwaggerUI( options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.DefaultModelsExpandDepth(-1);

    });
}


app.UseCors(MyAllowSpecificOrigins);


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Map all endpoints
app.MapEndpoints<Program>();

app.Run();

