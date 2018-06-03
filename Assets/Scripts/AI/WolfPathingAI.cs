using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfPathingAI : MonoBehaviour {

    Pathfinding pathfinding;
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer sprite;

    bool facingLeft;

    public float speed;
    public Transform player;
    Vector2 pathingOffset;

    public float pathfindingDelay;
    float delayCount;

    List<Node> targetPath;

	// Use this for initialization
	void Start () {
        pathfinding = Pathfinding.instance;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();        

        pathingOffset = new Vector2(0, -sprite.bounds.size.y / 2);

        facingLeft = false;
        delayCount = 0f;
    }
	
    void Update()
    {
        delayCount += Time.deltaTime;
        if (delayCount > pathfindingDelay)
        {
            Vector3 position = new Vector3(this.transform.position.x + pathingOffset.x, this.transform.position.y + pathingOffset.y, this.transform.position.z);
            if (pathfinding.JPS(position, player.position))
            {
                targetPath = pathfinding.GetPath(position, player.position);
            }
            delayCount = 0;
        }
    }


	// Update is called once per frame
	void FixedUpdate()
    {
        if (targetPath != null && targetPath.Count > 0)
        {
            float deltax = targetPath[0].position.x - (rb2d.position.x + pathingOffset.x);
            float deltay = targetPath[0].position.y - (rb2d.position.y + pathingOffset.y);
            float sqdistance = (deltax * deltax) + (deltay * deltay);

            float deltax2 = player.transform.position.x - rb2d.position.x;
            float deltay2 = player.transform.position.y - rb2d.position.y;
            float distanceFromPlayer = (deltax2 * deltax2) + (deltay2 * deltay2);

            int x = 0;
            int y = 0;

            if (distanceFromPlayer > 2 && sqdistance > 0.02)
            {
                deltax = Mathf.Max(Mathf.Abs(deltax), 0.02f) == 0.02f ? 0 : deltax;
                deltay = Mathf.Max(Mathf.Abs(deltay), 0.02f) == 0.02f ? 0 : deltay;

                x = deltax != 0 ? (int)Mathf.Sign(deltax) : 0;
                facingLeft = x < 0;
                y = deltay != 0 ? (int)Mathf.Sign(deltay) : 0;
            }
            else
            {
                targetPath.RemoveAt(0);
            }

            animator.SetBool("isMoving", x != 0 || y != 0);

            float currentSpeed = sqdistance > 500f ? speed * 1.5f : speed;
            animator.SetBool("isRunning", sqdistance > 500f);            

            rb2d.velocity = new Vector2(Mathf.Lerp(0, x * currentSpeed, 0.8f), Mathf.Lerp(0, y * currentSpeed, 0.8f));

            sprite.flipX = facingLeft;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 position = new Vector3(this.transform.position.x + pathingOffset.x, this.transform.position.y + pathingOffset.y, this.transform.position.z);

        if (targetPath != null && targetPath.Count > 0)
        {
            Gizmos.color = Color.red;

            Vector3 start = position;
            Vector3 end;

            foreach(Node n in targetPath)
            {
                end = n.position;

                Gizmos.DrawLine(start, end);
                start = end;
            }
        }
    }
}
