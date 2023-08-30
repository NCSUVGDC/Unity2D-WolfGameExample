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
    [SerializeField, Tooltip("The force applied during a dash")] float DashForce = 10;
    private Animator animator;
    bool isGrounded = false;
    bool AirJumpReady = false;
    bool DashReady = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(MovementInput * MovementSpeed, ForceMode2D.Force);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxFallSpeed));
        Debug.Log(rb.velocity + ", " + maxMoveSpeed);
        isGrounded = Physics2D.Linecast(this.transform.position, this.transform.position + new Vector3(0, -1.1f, 0), ~(1 << 3));
        if (isGrounded)
        {
            AirJumpReady = true;
            DashReady = true;
        }

    }

    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        MovementInput.x = input.x;
        if (Mathf.Abs(MovementInput.x) > 0)
        {
            animator.SetBool("Moving", true);
            if (MovementInput.x > 0)
            {
                animator.SetBool("FacingRight", true);
            } else
            {
                animator.SetBool("FacingRight", false);
            }
        } else
        {
            animator.SetBool("Moving", false);
        }
        
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (isGrounded && ctx.started)
        {

            rb.AddForce(new Vector2(0, 1) * JumpForce, ForceMode2D.Impulse);
        } else if (AirJumpReady && ctx.started)
        {
            rb.AddForce(new Vector2(0, 1) * JumpForce, ForceMode2D.Impulse);
            AirJumpReady = false;
        }
        Debug.Log("hello");
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (DashReady && ctx.started)
        {
            float XMovement = MovementInput.x;
            rb.AddForce(new Vector2(XMovement, 0) * DashForce, ForceMode2D.Impulse);
            DashReady = false;

        }
    }
}
