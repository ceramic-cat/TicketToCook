using TicketToCode.Core.Models;

namespace TicketToCode.Core.Services;
public class ShoppingListService
{
    private readonly List<RecipeIngredient> _shoppingList = new List<RecipeIngredient>();

    public IReadOnlyList<RecipeIngredient> ShoppingList => _shoppingList;

    public void AddToShoppingList(RecipeIngredient ingredient)
    {
        if (ingredient == null || ingredient.Ingredient == null)
        {
            throw new ArgumentNullException(nameof(ingredient), "Ingredient cannot be null");
        }

        var currentIngredient = _shoppingList.FirstOrDefault(i => i.Ingredient.Name == ingredient.Ingredient.Name);

        if (currentIngredient != null)
        {
            currentIngredient.Quantity += ingredient.Quantity;
        }
        else
        {

            _shoppingList.Add(ingredient);

        }
    }
    public void EditShoppingListQuantity(RecipeIngredient updatedIngredient)
    {
               var ingredient = _shoppingList.FirstOrDefault(i => i.Ingredient.Name == updatedIngredient.Ingredient.Name && i.Ingredient.Unit == updatedIngredient.Ingredient.Unit);

        if (ingredient != null)
        {
            
            if (updatedIngredient.Quantity <= 0)
            {
                _shoppingList.Remove(ingredient); 
            }
            else
            {
                
                ingredient.Quantity = updatedIngredient.Quantity;
            }
        }
    }
}