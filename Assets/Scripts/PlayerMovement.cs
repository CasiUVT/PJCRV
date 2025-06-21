using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;

            if (animator != null)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isRunning", horizontal != 0);
            }
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;

            if (animator != null)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isRunning", false);
            }
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            coyoteTimeCounter = 0f;

            if (animator != null)
            {
                animator.SetBool("isJumping", true); // Setează imediat la săritură
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", false);
            }
        }

        if (animator != null)
        {
            animator.SetBool("isIdle", isGrounded() && horizontal == 0);
        }

        Flip();
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
