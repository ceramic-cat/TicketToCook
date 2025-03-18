using System.ComponentModel;
using System.Reflection;

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

    public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            string description = attributes != null && attributes.Length > 0 ? attributes[0].Description : value.ToString();
            return $"{value.ToString()}: {description}";
        }
    }
}