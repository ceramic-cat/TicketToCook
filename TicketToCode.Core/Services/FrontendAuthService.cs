using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace TicketToCode.Core.Services;
public class FrontendAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRunTime;

    public FrontendAuthService(HttpClient httpClient, IJSRuntime jsRunTime)
    {
        _httpClient = httpClient;
        _jsRunTime = jsRunTime;
    }


    public async Task<bool> Login(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login",
            new { Username = username, Password = password });

        if (response.IsSuccessStatusCode)
        {
            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            if (tokenResponse != null)
            {
                await _jsRunTime.InvokeVoidAsync("localStorage.setItem", "authToken", tokenResponse.Token);
                return true;
            }
        }
        return false;
    }

    public async Task Logout()
    {
            var tokenAfterRemoval = await GetToken();
    }



    public async Task<string> GetToken()
    {
        return await _jsRunTime.InvokeAsync<string>("localStorage.getItem", "authToken");
    }
    public async Task<UserProfile?> GetUserProfile()
    {
        var token = await GetToken();

        if (string.IsNullOrEmpty(token))
            return null;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync("auth/fetch");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserProfile>();
        }

        return null;
    }
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
    }

    public class UserProfile
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}