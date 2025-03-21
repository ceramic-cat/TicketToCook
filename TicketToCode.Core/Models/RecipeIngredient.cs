
namespace TicketToCode.Core.Models;

    public class RecipeIngredient
    {
        public Ingredient Ingredient { get; set; }
        public double Quantity { get; set; }

        public RecipeIngredient(Ingredient ingredient, double quantity)
        {
            Ingredient = ingredient;
            Quantity = quantity;
        }

        public RecipeIngredient() { }
    }
