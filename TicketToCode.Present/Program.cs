using TicketToCode.Present.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp =>
{
    var handler = new HttpClientHandler
    {
        UseCookies = true, 
        AllowAutoRedirect = true,
        UseDefaultCredentials = false,
        Credentials = null,
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignore SSL issues (for local dev)
    };

    return new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001") };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
