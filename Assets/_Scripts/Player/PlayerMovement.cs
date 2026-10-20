using UnityEngine;

public class PlayerMovement : BaseMonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpForce = 2f;

    private Rigidbody2D rb2d;
    private PlayerAnimation playerAnimation;
    private float moveInput;

    public bool IsGrounded { get; private set; }
    public bool IsJumping { get; private set; }

    void FixedUpdate()
    {
        Move();
        RequestFall();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        LoadRigidbody();
        LoadAnimation();
    }


    public void OnGroundEnter()
    {
        IsGrounded = true;
        IsJumping = false;
    }

    public void OnGroundExit()
    {
        IsGrounded = false;
    }

    protected virtual void LoadRigidbody()
    {
        if (rb2d != null) return;

        rb2d = transform.parent.GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + ": LoadRigidbody", gameObject);
    }

    protected virtual void LoadAnimation()
    {
        if (playerAnimation != null) return;

        playerAnimation = transform.parent.GetComponentInChildren<PlayerAnimation>();
        Debug.Log(transform.name + ": LoadAnimation", gameObject);
    }

    public void SetMoveInput(float value)
    {
        moveInput = value;
    }

    public void RequestJump()
    {
        if (!IsGrounded) return;
        rb2d.linearVelocity = new Vector2(
            rb2d.linearVelocity.x,
            0f
        );

        rb2d.AddForce(
            Vector2.up * jumpForce,
            ForceMode2D.Impulse
        );

        Debug.Log("Jumping");
        IsJumping = true;
        playerAnimation.SetJumping(IsJumping);
    }

    private void RequestFall()
    {
        if(rb2d.linearVelocity.y > 0f) return; 
        if(rb2d.linearVelocity.y < 0f)
        {
            IsJumping = false;
            playerAnimation.SetJumping(IsJumping);
            playerAnimation.SetFalling(true);
        }
        else
        {
            RequestGrounded();
        }
    }

    private void RequestGrounded()
    {
        if(!IsGrounded) return;
        IsJumping = false;
        playerAnimation.SetJumping(IsJumping);
        playerAnimation.TriggerLand();
    }


    private void Move()
    {
        rb2d.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb2d.linearVelocity.y
        );

        playerAnimation.SetRunning(moveInput != 0f);

        if (moveInput > 0f)
        {
            playerAnimation.SetFacingRight(true);
        }
        else if (moveInput < 0f)
        {
            playerAnimation.SetFacingRight(false);
        }
    }

    
}