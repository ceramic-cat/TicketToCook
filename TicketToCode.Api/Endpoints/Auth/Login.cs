using TicketToCode.Core.Models;
using TicketToCode.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TicketToCode.Api.Endpoints.Auth;

public class Login : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/auth/login", Handle)
        .WithSummary("Login with username and password")
        .WithTags("User")
        .AllowAnonymous();

    // Models
    public record Request(string Username, string Password);
    public record Response(string Username, string Role, string Token);

    // Logic
    private static Results<Ok<Response>, NotFound<string>> Handle(
        Request request,
        IAuthService authService,
        HttpContext context,
        IConfiguration configuration)
    {
        var result = authService.Login(request.Username, request.Password);
        if (result == null)
        {
            return TypedResults.NotFound("Invalid username or password");
        }

        // Create token
        var token = GenerateJwtToken(result, configuration);


        var response = new Response(result.Username, result.Role, token);
        return TypedResults.Ok(response);
    }

    // JWT Token Generation
    private static string GenerateJwtToken(dynamic user, IConfiguration configuration)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "YourSecretKeyHere-MakeItLongAndComplex"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"] ?? "YourApi",
            audience: configuration["Jwt:Audience"] ?? "YourApp",
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
} 