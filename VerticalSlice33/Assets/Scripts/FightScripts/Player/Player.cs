using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 10f;

    public float jumpForce = 5f;
    public float lowJumpMultiplier = 2f;
    public float fallMultiplier = 2.5f;

    public float dashSpeed = 30f;

    protected Rigidbody2D rb;
    public bool isGrounded;
    private bool canDash = true;
    private bool isDashing;

    public bool facingRight = true;

    public GameObject parrySparks;
    public GameObject slash;

    public Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * currentSpeed;

        if(isDashing == false)
        {
            if (horizontalInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (horizontalInput < 0 && facingRight)
            {
                Flip();
            }
        }

        if (horizontalInput != 0 && isGrounded == true && isDashing == false)
        {
            anim.SetBool("run", true);
            anim.SetBool("idle", false);
            rb.velocity = new Vector2(movement.x, rb.velocity.y);
        }
        else
        {
            anim.SetBool("run", false);
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("dash", true);
            anim.SetBool("idle", false);
        }
        if(isDashing == true)
        {
            Physics2D.IgnoreLayerCollision(7, 8, true);
        }
        else Physics2D.IgnoreLayerCollision(7, 8, false);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("attack");
            anim.SetBool("idle", false);
        }

        Jump();

        if (Input.GetKeyDown(KeyCode.X))
        {
            parry();
        }
    }

    private void Dash()
    {
        anim.SetBool("idle", false);
        anim.SetBool("run", false);
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        isDashing = true;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void returnToIdle()
    {
        anim.SetBool("idle", true);
        anim.SetBool("dash", false);
        isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void parry()
    {
        Instantiate(parrySparks, transform.position, Quaternion.identity);
    }

    private void SlashAtk()
    {
        GameObject slashEffect = Instantiate(slash, transform.position, Quaternion.identity);

        if(facingRight == false)
        {
            Vector3 flippedScale = slashEffect.transform.localScale;
            flippedScale.x *= -1;
            slashEffect.transform.localScale = flippedScale;
        }

        Rigidbody2D rb = slashEffect.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction;

            if (facingRight == false)
            {
                direction = -1f * Vector2.right;
            }
            else
            {
                direction = Vector2.right;
            }
            
            rb.velocity = direction * 20f;
        }
    }
}
