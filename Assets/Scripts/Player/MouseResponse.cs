using UnityEngine;
using UnityEngine.EventSystems;

public class MouseResponse : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Animator animator;
    public Animator toolAnimator;

    Interactable focus;
    InventoryItem selectedTool;
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
                    selectedTool = ToolbarManager.instance.GetSelectedItem();

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
        animator.SetBool("PlayerSwinging", false);
        toolAnimator.SetInteger("ToolId", 0);
        buttonPressed = false;
    }

    void Update()
    {
        if (focus == null || !focus.isActive())
        {
            buttonPressed = false;
            animator.SetBool("PlayerSwinging", false);
            toolAnimator.SetInteger("ToolId", 0);
        }            

        if (!buttonPressed)
            return;

        clickDelay += Time.deltaTime;

        if (clickDelay < 0.3f)
            return;        

        clickDelay = 0f;
        PlayerController.instance.SetFocus(focus);

        if (focus.ToolRequired())
        {
            animator.SetBool("PlayerSwinging", true);

            if (selectedTool != null)
                toolAnimator.SetInteger("ToolId", selectedTool.item.id);
        }
    }
}