using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Singleton
    public static PlayerController instance;

    void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Multiple instances of player character");
            return;
        }

        instance = this;
    }
    #endregion

    private Animator animator;

    PlayerStatus playerStatus;
    Rigidbody2D rb2d;
    Interactable focus;
    SpriteRenderer sprite;

    float currentSpeed;

    void Start()
    {
        playerStatus = PlayerStatus.instance;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (DebugManager.instance.debug)
            return;

        currentSpeed = playerStatus.GetSpeed();
        animator.SetFloat("Movement", 0f);
        playerStatus.isRunning = false;        

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            float multiplier = 1.0f;            

            if (Input.GetButton("Sprint"))
            {
                multiplier = playerStatus.Run();
            }

            animator.SetFloat("Movement", multiplier);
            sprite.flipX = (Input.GetAxis("Horizontal") < 0);
        }

        rb2d.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * currentSpeed, 0.8f), Mathf.Lerp(0, Input.GetAxis("Vertical") * currentSpeed, 0.8f));
    }

    public void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.UnFocus();

            focus = newFocus;
        }

        newFocus.Focus(transform);
    }

    public void RemoveFocus()
    {
        if (focus != null)
            focus.UnFocus();

        focus = null;
    }
}