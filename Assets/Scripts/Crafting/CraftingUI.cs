using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    public GameObject inventoryObject;
    public GameObject craftingObject;

    Button[] inventoryItems;
    Button[] craftingItems;

    Inventory inventory;
    CraftingManager craftingManager;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateItemUI;

        craftingManager = CraftingManager.instance;
        craftingManager.onRecipeUpdateCallback += UpdateCraftingItemUI;

        inventoryItems = inventoryObject.GetComponentsInChildren<Button>();
        craftingItems = craftingObject.GetComponentsInChildren<Button>();

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < craftingItems.Length; i++)
        {
            craftingItems[i].gameObject.SetActive(false);
        }
    }

    void UpdateItemUI()
    {
        List<int> heldItems = new List<int>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            InventoryItem item = inventory.items[i];
            if (item != null && item.item.craftingItem && !heldItems.Contains(item.item.id))
            {
                int quantity = item.quantity;
                inventoryItems[i].gameObject.SetActive(true);
                for (int j = i + 1; j < inventoryItems.Length; j++)
                {
                    if (inventory.items[j] != null && inventory.items[j].item.id == item.item.id)
                    {
                        quantity += inventory.items[j].quantity;
                    }
                }
                heldItems.Add(item.item.id);
                inventoryItems[i].GetComponentInChildren<Text>().text = item.item.name + " : " + quantity;
                inventoryItems[i].GetComponent<CraftingSlot>().item = new CraftingItem(item.item, quantity);
            }
            else
                inventoryItems[i].gameObject.SetActive(false);
        }
    }

    void UpdateCraftingItemUI()
    {
        for (int i = 0; i < craftingManager.recipes.Count; i++)
        {
            craftingItems[i].GetComponentInChildren<Text>().text = craftingManager.recipes[i].craftedItem.name;
            craftingItems[i].gameObject.SetActive(true);
        }
    }
}