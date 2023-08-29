using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;

    private Rigidbody2D rb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dist = player.transform.position - transform.position;
        Vector2 dir = dist.normalized;

        if (dist.y > 1 && Mathf.Abs(dist.x) < 3 && Physics2D.Linecast(this.transform.position, this.transform.position + new Vector3(0, -1.1f, 0), ~(1 << 6)))
        {
            Debug.Log("gaming!!!");
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }
}
