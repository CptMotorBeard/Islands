using UnityEngine;
using UnityEngine.EventSystems;

public class MoveableUIObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool mouseDown;
    Transform parent;

    void Start()
    {
        parent = this.transform.parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseDown = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (mouseDown)
        {
            parent.position = Input.mousePosition;
        }
	}
}
