using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerDownHandler
{

    InventoryManager inventoryManager;
    Inventory inventory;

    void Awake()
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
            if (inventorySlot.storedItem == null && heldItem != null)
            {
                InventoryItem dropItem = new InventoryItem(heldItem.item, inventorySlot.index, 1);
                inventory.Add(dropItem);
                inventoryManager.DropItem(1);
                return;
            }
            if (inventorySlot.storedItem != null)
            {
                if (heldItem != null)
                {                    
                    if (heldItem.item.id == inventorySlot.storedItem.item.id)
                    {
                        inventory.Remove(inventorySlot.index, 1);
                        inventoryManager.PickupItem(1);
                    }
                }
                else
                {
                    heldItem = inventorySlot.storedItem;
                    inventory.Remove(inventorySlot.index, 1);
                    inventoryManager.PickupItem(new InventoryItem(heldItem.item, heldItem.inventorySlot, 1));
                }
            }            
        }

        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            InventoryItem heldItem = inventoryManager.currentItem;
            if (heldItem != null)
            {
                if (inventorySlot.storedItem == null)
                {
                    inventoryManager.ClearItem();
                    heldItem.inventorySlot = inventorySlot.index;
                    inventory.Add(heldItem);
                }
                else
                {
                    if (heldItem.item.id == inventorySlot.storedItem.item.id)
                    {
                        int total, overflow;
                        total = heldItem.quantity + inventorySlot.storedItem.quantity;
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
                }
            }
            else
            {
                inventorySlot.UseItem();
            }
        }
    }
}
