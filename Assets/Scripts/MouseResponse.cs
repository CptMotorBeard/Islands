using UnityEngine;
using UnityEngine.EventSystems;

public class MouseResponse : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Interactable focus;
    bool buttonPressed = false;
    float clickDelay = 0f;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Vector2 WorldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(WorldMouse, Vector2.zero, 0f);
            if (hit)
            {
                // Right click on an object
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable)
                {
                    // If object is interactable set it as our focus
                    focus = interactable;
                    buttonPressed = true;
                }
            }
            else
            {
                PlayerController.instance.RemoveFocus();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    void Update()
    {        
        if (!buttonPressed)
            return;

        clickDelay += Time.deltaTime;

        if (clickDelay < 0.3f)
            return;

        clickDelay = 0f;
        PlayerController.instance.SetFocus(focus);
    }
}