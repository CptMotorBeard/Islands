using UnityEngine;

public class Character : MonoBehaviour {

    public float speed;
    public Rigidbody2D player;

    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer sprite;

    bool facingLeft;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        facingLeft = false;
    }
	
	void FixedUpdate () {
        float deltax = player.position.x - rb2d.position.x;
        float deltay = player.position.y - rb2d.position.y;
        float sqdistance = (deltax * deltax) + (deltay * deltay);

        int x = 0;
        int y = 0;

        if (sqdistance > 10)
        {
            deltax = Mathf.Max(Mathf.Abs(deltax), 0.5f) == 0.5f ? 0 : deltax;
            deltay = Mathf.Max(Mathf.Abs(deltay), 0.5f) == 0.5f ? 0 : deltay;

            x = deltax != 0 ? (int)Mathf.Sign(deltax) : 0;
            facingLeft = x < 0;
            y = deltay != 0 ? (int)Mathf.Sign(deltay) : 0;
        }        

        animator.SetBool("isMoving", x != 0 || y != 0);

        float currentSpeed = sqdistance > 500f ? speed * 1.5f : speed;
        animator.SetBool("isRunning", sqdistance > 500f);

        if (Input.GetButton("Sprint"))
        {
            currentSpeed = speed * 1.5f;
            animator.SetBool("isRunning", x != 0 || y != 0);
        }

        rb2d.velocity = new Vector2(Mathf.Lerp(0, x * currentSpeed, 0.8f), Mathf.Lerp(0, y * currentSpeed, 0.8f));

        sprite.flipX = facingLeft;
    }
}
