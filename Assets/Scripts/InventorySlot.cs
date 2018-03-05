using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


    // Inventory Slots need to use InventoryItems and not Items
    // Right now it works ok, but cannot drop items or get quantity
    public InventoryItem storedItem;
    public Image icon;
    public TextMeshProUGUI tooltip;
    public TextMeshProUGUI quantity;
    public Image dropButton;
    public int index;

    public void AddItem(InventoryItem newItem)
    {
        storedItem = newItem;

        quantity.text = storedItem.quantity.ToString();

        icon.sprite = storedItem.item.icon;
        icon.enabled = true;
        dropButton.enabled = true;
    }

    public void AddItem(InventoryItem newItem, int q)
    {
        storedItem = newItem;
        storedItem.quantity = q;

        quantity.text = storedItem.quantity.ToString();

        icon.sprite = storedItem.item.icon;
        icon.enabled = true;
        dropButton.enabled = true;
    }

    public void ClearSlot()
    {
        storedItem = null;

        quantity.text = "";

        icon.sprite = null;
        icon.enabled = false;
        dropButton.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (storedItem == null)
            return;

        tooltip.text = storedItem.item.name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.text = "";
    }

    public void UseItem()
    {
        if (storedItem == null)
            return;

        bool consumed = storedItem.item.Use();
        if (consumed)
            storedItem.Remove(1);
    }

    public void DropItem()
    {
        if (storedItem == null)
            return;

        storedItem.Remove(storedItem.quantity);
    }

    public void RemoveItem(int quantity)
    {
        if (storedItem.quantity >= quantity)
        {
            storedItem.Remove(quantity);
        }
    }
}
