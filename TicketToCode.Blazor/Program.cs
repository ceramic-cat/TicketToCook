using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TicketToCode.Blazor;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });

builder.Services.AddScoped<TicketToCode.Core.Services.FrontendAuthService>();
builder.Services.AddScoped<TicketToCode.Core.Services.FavoriteService>();
builder.Services.AddScoped<TicketToCode.Core.Services.ShoppingListService>();
builder.Services.AddScoped<TicketToCode.Core.Services.NavState>();


var host = builder.Build();

// Clear the token on application start
var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
await jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");

await host.RunAsync();