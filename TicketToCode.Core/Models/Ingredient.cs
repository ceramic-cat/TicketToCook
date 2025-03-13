using TicketToCode.Core.Enums;

namespace TicketToCode.Core.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IngredientType Type { get; set; }
        public double Quantity { get; set; }
        public MeasurementUnit Unit { get; set; }

        public Ingredient(string name, IngredientType type, MeasurementUnit unit)
        {
            Name = name;
            Type = type;
            Unit = unit;

        }
    }
}
