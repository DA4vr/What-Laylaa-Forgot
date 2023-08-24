using UnityEngine;

// This script is a basic 2D character controller that allows
// the player to run and jump. It uses Unity's new input system,
// which needs to be set up accordingly for directional movement
// and jumping buttons.
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Params")]
    public float runSpeed = 216.0f;
    public float jumpSpeed = 318.0f;
    public float gravityScale = 20.0f;

 
    // components attached to player
    private CapsuleCollider2D coll;
    private Rigidbody2D rb;

    // other
    private bool isGrounded = false;
 
    private void Awake()
    {
        coll = GetComponent < CapsuleCollider2D >();
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = gravityScale;
    }

    private void FixedUpdate()
    {
        //if (DialogueManager.GetInstance().dialogueIsPlaying)
        //{
        //    return;
        //}

        UpdateIsGrounded();

        HandleHorizontalMovement();

        HandleJumping();
    }

    private void UpdateIsGrounded()
    {

        Debug.Log("Update is grounded");
        Bounds colliderBounds = coll.bounds;
        float colliderRadius = coll.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        // Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        this.isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != coll)
                {
                    this.isGrounded = true;
                    break;
                }
            }
        }
    }

    private void HandleHorizontalMovement()
    {
        Debug.Log("moving through characterMovement");
        Vector2 moveDirection = InputManager.GetInstance().GetMoveDirection();
        rb.velocity = new Vector2(moveDirection.x * runSpeed, rb.velocity.y);
    }

    private void HandleJumping()
    {
        bool jumpPressed = InputManager.GetInstance().GetJumpPressed();
        if (isGrounded && jumpPressed)
        {
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

}