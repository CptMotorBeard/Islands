using UnityEngine;

public class ToolbarUI : MonoBehaviour {

    public Transform toolbarParent;

    Inventory inventory;
    InventorySlot[] slots;

    const int startPoint = Inventory.space;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = toolbarParent.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].index = startPoint + i;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (inventory.items[startPoint + i] != null)
                slots[i].AddItem(inventory.items[startPoint + i]);
            else
                slots[i].ClearSlot();
        }
    }
}
