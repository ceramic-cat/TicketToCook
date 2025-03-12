namespace TicketToCode.Core.Models;

public class ShoppingListItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }

    public ShoppingListItem(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}