using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 MovementInput = new Vector2();
    [SerializeField, Tooltip("standard movement speed")] float MovementSpeed = 10;
    [SerializeField, Tooltip("dashing movement speed")] float DashSpeed = 20;
    [SerializeField, Tooltip("the max vertical move speed")] float maxFallSpeed = 10;
    [SerializeField, Tooltip("The force applied during a jump")] float JumpForce = 10;
    [SerializeField, Tooltip("The duration of a dash")] float DashDuration = 0.2f;
    [SerializeField, Tooltip("The cooldown of a dash")] float DashCooldown = 0.5f;
    private Animator animator;
    bool isGrounded = false;
    bool AirJumpReady = false;
    bool DashReady = false;
    float dashTime = 0;
    float dashRefresh = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //  rb.AddForce(MovementInput * MovementSpeed, ForceMode2D.Force);
        if (dashTime > 0)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(MovementInput.x * DashSpeed,  Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxFallSpeed)), Time.fixedDeltaTime * 10);
            dashTime -= Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(MovementInput.x * MovementSpeed, Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxFallSpeed)), Time.fixedDeltaTime * 10); 
        }

        isGrounded = Physics2D.Linecast(this.transform.position, this.transform.position + new Vector3(0, -1.1f, 0), ~(1 << 3));
        /**
        if (isGrounded)
        {
            rb.sharedMaterial.friction = 2;
            rb.sharedMaterial = rb.sharedMaterial;
            AirJumpReady = true;
        } else
        {
            rb.sharedMaterial.friction = 0;
            rb.sharedMaterial = rb.sharedMaterial;
        }
        */

        if (dashRefresh > 0)
            dashRefresh -= Time.deltaTime;
        else if (isGrounded)
            DashReady = true;

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
        }
        else if (AirJumpReady && ctx.started)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, 1) * JumpForce, ForceMode2D.Impulse);
            AirJumpReady = false;
        }
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (DashReady && ctx.started)
        {
            Debug.Log("test");
            float XMovement = MovementInput.x;

            if (XMovement == 0)
                XMovement = animator.GetBool("FacingRight") ? 1 : -1;


            DashReady = false;
            dashTime = DashDuration;
            dashRefresh = DashCooldown;
        }
    }
}
