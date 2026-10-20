using UnityEngine;

public class PlayerCtrl : SingletonPattern<PlayerCtrl>
{
    [SerializeField] private PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;
    [SerializeField] private Rigidbody2D rb2d;
    public Rigidbody2D Rb2d => rb2d;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    [SerializeField] private PlayerAnimation playerAnimation;
    public PlayerAnimation PlayerAnimation => playerAnimation;
    [SerializeField] private PlayerInput playerInput;
    public PlayerInput PlayerInput => playerInput;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerMovement();
        this.LoadRigidbody2D();
        this.LoadSpriteRenderer();
        this.LoadAnimator();
        this.LoadPlayerAnimation();
        this.LoadPlayerInput();
    }

    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        this.playerMovement = GetComponentInChildren<PlayerMovement>();
        Debug.Log(transform.name + ": Load Player Movement", gameObject);
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this.rb2d != null) return;
        this.rb2d = GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + ": Load Rigidbody2D", gameObject);
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this.spriteRenderer != null) return;
        this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Debug.Log(transform.name + ": Load Sprite Renderer", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();
        Debug.Log(transform.name + ": Load Animator", gameObject);
    }

    protected virtual void LoadPlayerAnimation()
    {
        if (this.playerAnimation != null) return;
        this.playerAnimation = GetComponentInChildren<PlayerAnimation>();
        Debug.Log(transform.name + ": Load Player Animation", gameObject);
    }

    protected virtual void LoadPlayerInput()
    {
        if (this.playerInput != null) return;
        this.playerInput = GetComponentInChildren<PlayerInput>();
        Debug.Log(transform.name + ": Load Player Input", gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            this.playerMovement.OnGroundEnter();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            this.playerMovement.OnGroundExit();
        }
    }
}
