using System.ComponentModel;

namespace TicketToCode.Core.Enums
{
    public enum Category
    {
        [Description("Vegan recipes with no animal products.")]
        Vegan,

        [Description("Sweet dishes typically served after a meal.")]
        Dessert,

        [Description("Traditional dishes from Italy.")]
        Italian,

        [Description("Recipes suitable for gluten-sensitive individuals.")]
        GlutenFree,

        [Description("Low-carb recipes that focus on healthy fats and proteins.")]
        Keto,

        [Description("High-protein recipes ideal for muscle building.")]
        HighProtein,

        [Description("Low-carb meals, suitable for weight management.")]
        LowCarb,

        [Description("Quick and easy meals for busy schedules.")]
        QuickMeal,

        [Description("Meals that are loved by the whole family.")]
        FamilyFriendly,

        [Description("Special recipes for holiday celebrations.")]
        Holiday,

        [Description("Delicious seafood-based dishes.")]
        Seafood,

        [Description("Slow-cooked meals for maximum flavor.")]
        SlowCooker
    }

   
}