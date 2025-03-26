using System.Net.Http.Json;

using TicketToCode.Core.Models;

namespace TicketToCode.Core.Services;
public class FavoriteService
{
    private readonly HttpClient _httpClient;
    private readonly FrontendAuthService _authService;

    public FavoriteService(HttpClient httpClient, FrontendAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    private async Task SetAuthHeader()
    {
        var token = await _authService.GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

        public async Task<List<RecipeDto>> GetFavoritesAsync()
    {
         try
        {
            await SetAuthHeader();

            var response = await _httpClient.GetAsync("user/favorites");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<FavoritesResponse>();
                return result?.Favorites ?? new List<RecipeDto>();
            }

            return new List<RecipeDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting favorites: {ex.Message}");
            return new List<RecipeDto>();
        }
    }

    public async Task<bool> ToggleFavoriteAsync(int recipeId)
    {
        try
        {
            await SetAuthHeader();

            var response = await _httpClient.PostAsync($"user/favorites/toggle/{recipeId}", null);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ToggleResponse>();
                return result?.IsFavorite ?? false;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error toggling favorite: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> IsRecipeFavoriteAsync(int recipeId)
    {
        try
        {
            await SetAuthHeader();

            var response = await _httpClient.GetAsync($"user/favorites/check/{recipeId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IsFavoriteResponse>();
                return result?.IsFavorite ?? false;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking favorite status: {ex.Message}");
            return false;
        }
    }

    public class IsFavoriteResponse
    {
        public bool IsFavorite { get; set; }
    }

   public class FavoritesResponse
    {
        public List<RecipeDto> Favorites { get; set; } = new();
    }

    public record RecipeDto(int Id, string Name, Category Category);

    public class ToggleResponse
    {
        public bool IsFavorite { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}