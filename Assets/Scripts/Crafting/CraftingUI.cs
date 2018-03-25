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

        for (int i = 0; i < craftingItems.Length; i++)
        {
            craftingItems[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i].gameObject.SetActive(false);
        }
    }

    void UpdateItemUI()
    {
        // Clearing the crafting manager only clears the buttons used
        craftingManager.Clear();

        List<int> heldItems = new List<int>();
        // We have an equal number of inventoryItems to item slots in the inventory
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            InventoryItem item = inventory.items[i];
            CraftingSlot slot = inventoryItems[i].GetComponent<CraftingSlot>();
            inventoryItems[i].GetComponent<Image>().color = Color.white;
            slot.selected = false;

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
                slot.item = new CraftingItem(item.item, quantity);

                // Check to see if the button is part of the currently selected recipe, if it is, select the button and add it back to the crafting button list
                if (craftingManager.inRecipe(item.item.id))
                {
                    craftingManager.Add(slot);
                    inventoryItems[i].GetComponent<Image>().color = Color.black;
                    slot.selected = true;
                }
            }
            else
            {
                slot.item = new CraftingItem(null, 0);
                inventoryItems[i].gameObject.SetActive(false);
            }

        }
    }

    void UpdateCraftingItemUI()
    {
        for (int i = 0; i < craftingItems.Length; i++)
        {
            if (i < craftingManager.recipes.Count)
            {
                craftingItems[i].GetComponentInChildren<Text>().text = craftingManager.recipes[i].craftedItem.name;
                craftingItems[i].GetComponent<CraftingRecipeButton>().recipe = craftingManager.recipes[i];
                craftingItems[i].gameObject.SetActive(true);
            }
            else
            {
                craftingItems[i].gameObject.SetActive(false);
            }
        }
    }
}