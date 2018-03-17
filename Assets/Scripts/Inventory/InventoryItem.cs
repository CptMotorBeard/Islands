public class InventoryItem
{
    public Item item;
    public int inventorySlot;
    public int quantity;
    

    public InventoryItem(Item item, int inventorySlot, int quantity)
    {
        this.item = item;
        this.inventorySlot = inventorySlot;
        this.quantity = quantity;
    }

    public int Add(int amount)
    {
        quantity += amount;

        if (quantity > item.maxQuantity)
        {
            int overflow = quantity - item.maxQuantity;
            quantity = item.maxQuantity;

            return overflow;
        }

        return 0;
    }

    public void Remove(int amount)
    {
        if (amount > quantity)
            amount = quantity;

        quantity -= amount;

        Inventory.instance.QuantityChanged(quantity, inventorySlot);        
    }
}