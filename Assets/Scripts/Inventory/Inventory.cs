using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("More than one instance of inventory");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public const int space = 28;
    public const int totalSpace = space + 10;   // for toolbar

    public InventoryItem[] items = new InventoryItem[totalSpace];

    public bool Add(Item item, int quantity)
    {
        int index = space - 1;
        for (int i = space - 1; i >= 0; i--)
        {
            if (items[i] == null)
                index = i;
            else
            {
                if (items[i].item.id == item.id)
                {
                    if (items[i].Add(quantity) == 0)
                    {
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();
                        return true;
                    }
                }
            }
        }
        if (items[index] != null)
        {
            MessageManagement.instance.SetErrorMessage("Not enough space");
            return false;
        }

        items[index] = new InventoryItem(item, index, quantity);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        return true;
    }

    public bool Add(InventoryItem item)
    {
        int index = item.inventorySlot;

        if (items[index] == null)
        {
            items[index] = item;
        }
        else if (items[index].item.id == item.item.id)
        {
            int overflow = items[index].Add(item.quantity);
            if (overflow > 0)
                return false;
        }
        else
            return false;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(int index)
    {
        items[index] = null;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void Remove(int index, int q)
    {
        items[index].quantity -= q;
        if (items[index].quantity <= 0)
        {
            items[index] = null;
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void QuantityChanged(int newQuantity, int inventorySlot)
    {
        if (newQuantity <= 0)
        {
            Remove(inventorySlot);
        }
        else
        {
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
    }

    public void RemoveItem(Item item, int quantity)
    {
        int i = space;
        while (quantity > 0 && i >= 0)
        {
            i--;
            if (items[i] != null && items[i].item.id == item.id)
            {
                int itemQuantity = items[i].quantity;
                Remove(i, quantity);
                quantity -= itemQuantity;
            }
        }
        if (i < 0 && quantity > 0)
        {
            Debug.LogWarning("Somethine went terribly wrong with removing items");
            MessageManagement.instance.SetErrorMessage("Something went terribly wrong with removing items");
        }
    }
}
