using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TicketToCode.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });

builder.Services.AddScoped<TicketToCode.Core.Services.FrontendAuthService>();
builder.Services.AddScoped<TicketToCode.Core.Services.FavoriteService>();
builder.Services.AddScoped<TicketToCode.Core.Services.ShoppingListService>();
builder.Services.AddScoped<TicketToCode.Core.Services.NavState>();


await builder.Build().RunAsync();
