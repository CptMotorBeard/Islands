using UnityEngine;

public class Character : MonoBehaviour {

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
        sprite.flipX = facingLeft;
	}
}
