using TicketToCode.Core.Models;

namespace TicketToCode.Core.Services;
public class ShoppingListService
    {
        private readonly List<RecipeIngredient> _shoppingList = new List<RecipeIngredient>();

        public IReadOnlyList<RecipeIngredient> ShoppingList => _shoppingList;

        public void AddToShoppingList(RecipeIngredient ingredient)
        {
            _shoppingList.Add(ingredient);
        }
    }