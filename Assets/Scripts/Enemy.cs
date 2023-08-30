using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] GameObject ball;

    public bool attack;

    private Rigidbody2D rb;
    private GameObject player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            Vector2 dist = player.transform.position - transform.position;
            Vector2 dir = dist.normalized;

            if (dist.y > 1 && Mathf.Abs(dist.x) < 3 && Physics2D.Linecast(this.transform.position, this.transform.position + new Vector3(0, -1.1f, 0), ~(1 << 6)))
            {
                Debug.Log("gaming!!!");
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }

            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

            animator.SetBool("FaceLeft", dist.x < 0);
        }

        animator.SetBool("Walking", rb.velocity.magnitude > 0);
        
        if (attack)
            Attack();
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Instantiate(ball, transform.position, transform.rotation);
        ball.GetComponent<Ball>().SetDirection(animator.GetBool("FaceLeft") ? -1 : 1);
        ball.GetComponent<Ball>().Go();

        attack = false;
    }
}
