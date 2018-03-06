using UnityEngine;

public class PlayerController : MonoBehaviour {

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
        rb2d = GetComponent<Rigidbody2D>();
    }
    #endregion

    PlayerStatus playerStatus;

    void Start()
    {
        playerStatus = PlayerStatus.instance;
    }

    Rigidbody2D rb2d;
    Interactable focus;

    float currentSpeed;


    void FixedUpdate()
    {
        currentSpeed = playerStatus.GetSpeed();
        playerStatus.isRunning = false;

        if (Input.GetButton("Sprint"))
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                playerStatus.Run();
            }
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