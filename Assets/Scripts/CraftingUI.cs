using UnityEngine;

public class CraftingUI : MonoBehaviour {
    public GameObject inventoryItems;
    public GameObject craftingRecipes;

    public GameObject inventoryItem;
    public GameObject craftingRecipe;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += FillItems;
    }

    void FillItems()
    {       

        for (int i = 0; i < inventory.items.Length; i++)
        {
            InventoryItem currentItem = inventory.items[i];            

            if (currentItem != null)
            {
                int quantity = currentItem.quantity;
                for (int j = i; j < inventory.items.Length; j++)
                {
                    if (inventory.items[j] != null && inventory.items[j].item.id == currentItem.item.id)
                    {
                        quantity += inventory.items[j].quantity;
                    }
                }
                GameObject o = Instantiate(inventoryItem);
                o.transform.SetParent(inventoryItems.transform);
            }
        }
    }
}