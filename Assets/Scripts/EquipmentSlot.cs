using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    Item item;
    public Image icon;
    public Sprite defaultIcon;

    TooltipBehavior tooltip;

    void Start()
    {
        tooltip = TooltipBehavior.instance;

        if (item == null)
            icon.sprite = defaultIcon;
    }

    public void EquipItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
    }

    public void RemoveItem()
    {
        if (item == null)
            return;

        item = null;
        icon.sprite = defaultIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
            return;

        tooltip.SetTooltip(item.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.ClearTooltip();
    }
}
