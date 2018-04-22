using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerDownHandler
{

    InventoryManager inventoryManager;
    Inventory inventory;

    void Start()
    {
        inventoryManager = InventoryManager.instance;
        inventory = Inventory.instance;
    }

    public InventorySlot inventorySlot;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            InventoryItem heldItem = inventoryManager.currentItem;

            if (inventorySlot.index >= Inventory.space)
            {
                if (inventorySlot.storedItem == null && heldItem != null)
                    DropAll(heldItem);
                else if ((inventorySlot.storedItem != null && heldItem == null))
                    PickupAll(inventorySlot.storedItem);

                return;
            }

            // No item in the slot, holding an item
            if (inventorySlot.storedItem == null && heldItem != null)
            {
                DropSingle(heldItem);
                return;
            }
            // Item currently in place
            if (inventorySlot.storedItem != null)
            {
                // Holding an item
                if (heldItem != null)
                {
                    // Item is same as one in place
                    if (heldItem.item.id == inventorySlot.storedItem.item.id)
                    {
                        PickupSingle(heldItem, false);
                    }
                }
                // Not holding an item
                else
                {
                    PickupSingle(inventorySlot.storedItem, true);
                }
            }
        }

        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (inventorySlot.index >= Inventory.space)
            {
                ToolbarManager.instance.SelectSlot(inventorySlot.index - Inventory.space);
                return;
            }

            InventoryItem heldItem = inventoryManager.currentItem;
            // Left click, held item
            if (heldItem != null)
            {
                // Slot empty or held item is slot item
                if (inventorySlot.storedItem == null || (heldItem.item.id == inventorySlot.storedItem.item.id))
                {
                    DropAll(heldItem);
                }
            }
            // Left click, not holding item
            else
            {
                inventorySlot.UseItem();
            }
        }
    }

    void DropSingle(InventoryItem heldItem)
    {
        InventoryItem dropItem = new InventoryItem(heldItem.item, inventorySlot.index, 1);
        inventory.Add(dropItem);
        inventoryManager.DropItem(1);
    }

    void DropAll(InventoryItem heldItem)
    {
        int total, overflow;
        total = heldItem.quantity;
        total += (inventorySlot.storedItem == null) ? 0 : inventorySlot.storedItem.quantity;

        overflow = total - heldItem.item.maxQuantity;

        heldItem.inventorySlot = inventorySlot.index;

        inventory.Add(heldItem);

        inventoryManager.ClearItem();

        if (overflow > 0)
        {
            heldItem.quantity = overflow;
            inventoryManager.PickupItem(heldItem);
        }
    }

    void PickupSingle(InventoryItem heldItem, bool newItem)
    {
        inventory.Remove(inventorySlot.index, 1);

        if (newItem)
            inventoryManager.PickupItem(new InventoryItem(heldItem.item, heldItem.inventorySlot, 1));
        else
            inventoryManager.PickupItem(1);
    }

    void PickupAll(InventoryItem heldItem)
    {
        inventoryManager.PickupItem(new InventoryItem(heldItem.item, heldItem.inventorySlot, inventorySlot.storedItem.quantity));
        inventory.Remove(inventorySlot.index, inventorySlot.storedItem.quantity);
    }
}
