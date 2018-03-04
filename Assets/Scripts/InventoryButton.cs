using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerDownHandler {

    public InventorySlot inventorySlot;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("right click");
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("left click");
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            Debug.Log("middle click");
        }
    }
}
