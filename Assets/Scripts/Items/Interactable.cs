using UnityEngine;

public class Interactable : MonoBehaviour {
    public float radius = 3f;

    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {

    }

    void Update()
    {
        BaseUpdate();
    }

    // Can't actually change update to virtual and this is my update. I may be making all interactables able to respawn in the future though so this may change
    public virtual void BaseUpdate()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector2.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
            else
            {
                UnFocus();
            }
        }
    }

    public void Focus(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void UnFocus()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }


    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public virtual bool ToolRequired()
    {
        return false;
    }

    public virtual bool isActive()
    {
        return true;
    }
}
