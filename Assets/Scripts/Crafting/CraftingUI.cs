using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    public GameObject inventoryObject;
    public GameObject craftingObject;

    Button[] inventoryItems;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        inventoryItems = inventoryObject.GetComponentsInChildren<Button>();

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i].gameObject.SetActive(false);
        }
    }

    void UpdateUI()
    {
        List<int> heldItems = new List<int>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            InventoryItem item = inventory.items[i];
            if (item != null && item.item.craftingItem && !heldItems.Contains(item.item.id))
            {
                int quantity = item.quantity;
                inventoryItems[i].gameObject.SetActive(true);
                for (int j = i+1; j < inventoryItems.Length; j++)
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
}