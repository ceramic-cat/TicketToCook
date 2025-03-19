using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using TicketToCode.Core.Models;

namespace TicketToCode.Core.Data;

public interface IDatabase
{
    List<User> Users { get; set; }
    List<Ingredient> Ingredients { get; set; }
    List<Recipe> Recipes { get; set; }
}

public class Database : IDatabase
{
    public List<User> Users { get; set; } = new List<User>();
    public List<Ingredient> Ingredients { get; set; }
    public List<Recipe> Recipes { get; set; }

    public Database()
    {
        // Lägg till en testanvändare
        Users.Add(new User("admin", BCrypt.Net.BCrypt.HashPassword("admin123")));

        AddSampleData();

    }
    public void AddSampleData()
    {
        // allow "wrong" casing for json serialization
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        // Get path to json files
        string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "TicketToCode.Core", "Data");

        string IngredientsPath = Path.Combine(dataPath, "SampleIngredients.json");
        string RecipesPath = Path.Combine(dataPath, "SampleRecipes.json");

        // Turn json file to string
        string IngredientsJson = File.ReadAllText(IngredientsPath);
        string RecipesJson = File.ReadAllText(RecipesPath);

        // Deserialize string to sample data (or new empty list)
        Ingredients = JsonSerializer.Deserialize<List<Ingredient>>(IngredientsJson, options) ?? new();
        Recipes = JsonSerializer.Deserialize<List<Recipe>>(RecipesJson, options) ?? new();
    }
    
}
