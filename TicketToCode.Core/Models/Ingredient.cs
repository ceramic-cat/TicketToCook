using System.ComponentModel;

namespace TicketToCode.Core.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IngredientType Type { get; set; }

        public Ingredient(string name, IngredientType type)
        {
            Name = name;
            Type = type;
        }

    }

    public enum IngredientType
    {
        [Description("Animal-based products")]
        Meat,

        [Description("Milk-based products")]
        Dairy,

        [Description("Plant-based products")]
        Vegetable,

        [Description("Grain-based products")]
        Grain,

        [Description("Fruit-based products")]
        Fruit,

        [Description("Seafood-based products")]
        Seafood,

        [Description("Spices and herbs")]
        Spice,

        [Description("Nuts and seeds")]
        Nut,

        [Description("Oils and fats")]
        Oil,

        [Description("Beverages")]
        Beverage,

        [Description("Condiments and sauces")]
        Condiment
    }
}