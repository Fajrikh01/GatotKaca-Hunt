using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    
    [SerializeField] private LayerMask jumpableGround;

    //private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private bool doubleJump;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalInput;

    private enum MovementState { idle, walking, jumping, falling }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        moveLeft = false;
        moveRight = false;
    }

    public void PointerDownLeft()
    {
        moveLeft = true;
    }

    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    public void PointerDownRight()
    {
        moveRight = true;
    }

    public void PointerUpRight()
    {
        moveRight = false;
    }

    private void Update()
    {
        //dirX = Input.GetAxisRaw("Horizontal");
        //rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        rb.velocity = new Vector2(horizontalInput, rb.velocity.y);
        UpdateAnimationUpdate();
    }

    private void UpdateAnimationUpdate()
    {
        MovementState state;

        if (moveRight)
        {
            horizontalInput = moveSpeed;
            state = MovementState.walking;
            sprite.flipX = false;
        }
        else if (moveLeft)
        {
            horizontalInput = -moveSpeed;
            state = MovementState.walking;
            sprite.flipX = true;
        }
        else
        {
            horizontalInput = 0;
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
        
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            dbJump();
            doubleJump = true;

        }
        else if (doubleJump)
        {
            dbJump();
            doubleJump = false;
        }
    }
    
    void dbJump()
    {
        SoundManager.instance.Play("Jump");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    
    public bool canAttack()
    {
        return horizontalInput == 0 && IsGrounded();
    }
}
