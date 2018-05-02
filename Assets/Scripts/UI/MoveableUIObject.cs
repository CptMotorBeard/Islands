using UnityEngine;
using UnityEngine.EventSystems;

public class MoveableUIObject : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    Transform parent;
    Vector3 offset;

    void Start()
    {
        parent = this.transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        parent.position = Input.mousePosition + offset;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 clickedPos = Input.mousePosition;
        offset = parent.position - clickedPos;
    }
}