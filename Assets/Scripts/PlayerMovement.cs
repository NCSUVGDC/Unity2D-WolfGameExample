using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 MovementInput = new Vector2();
    [SerializeField, Tooltip("standard movement speed")] float MovementSpeed = 5;
    [SerializeField, Tooltip("max horizontal move speed")] float maxMoveSpeed = 8;
    [SerializeField, Tooltip("the max vertical move speed")] float maxFallSpeed = 10;
    [SerializeField, Tooltip("The force applied during a jump")] float JumpForce = 10;
    bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(MovementInput * MovementSpeed, ForceMode2D.Force);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxFallSpeed));
        Debug.Log(rb.velocity + ", " + maxMoveSpeed);
        isGrounded = Physics2D.Linecast(this.transform.position, this.transform.position + new Vector3(0, -1.1f, 0), ~(1 << 3));
        Debug.Log(isGrounded);

    }

    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        MovementInput.x = input.x;
        
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (isGrounded && ctx.started)
        {

            rb.AddForce(new Vector2(0, 1) * JumpForce, ForceMode2D.Impulse);
        }
        Debug.Log("hello");
    }
}
