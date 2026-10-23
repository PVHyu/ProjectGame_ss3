using UnityEngine;

public class PigCtrl : SingletonPattern<PigCtrl>
{
    [SerializeField] private PigMovement pigMovement;
    public PigMovement PigMovement => pigMovement;
    [SerializeField] private Rigidbody2D rb2d;
    public Rigidbody2D Rb2d => rb2d;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    [SerializeField] private PigAnimation pigAnimation;
    public PigAnimation PigAnimation => pigAnimation;
    [SerializeField] private ManagingSkills managingSkills;
    public ManagingSkills ManagingSkills => managingSkills;
    [SerializeField] private PigHealth pigHealth;
    public PigHealth PigHealth => pigHealth;
    [SerializeField] private EnemyData enemyData;
    public EnemyData EnemyData => enemyData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPigMovement();
        this.LoadRigidbody2D();
        this.LoadSpriteRenderer();
        this.LoadAnimator();
        this.LoadPigAnimation();
        this.LoadPigHealth();
        this.LoadManagingSkills();
    }

    protected virtual void LoadPigMovement()
    {
        if (this.pigMovement != null) return;
        this.pigMovement = GetComponentInChildren<PigMovement>();
        Debug.Log(transform.name + ": Load Pig Movement", gameObject);
    }

    protected virtual void LoadPigHealth()
    {
        if (this.pigHealth != null) return;
        this.pigHealth = GetComponentInChildren<PigHealth>();
        Debug.Log(transform.name + ": Load Player Health", gameObject);
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

    protected virtual void LoadPigAnimation()
    {
        if (this.pigAnimation != null) return;
        this.pigAnimation = GetComponentInChildren<PigAnimation>();
        Debug.Log(transform.name + ": Load Pig Animation", gameObject);
    }

    protected virtual void LoadManagingSkills()
    {
        if (this.managingSkills != null) return;
        this.managingSkills = GetComponentInChildren<ManagingSkills>();
        Debug.Log(transform.name + ": Load Player Skills", gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // this.pigMovement.OnGroundEnter();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // this.pigMovement.OnGroundExit();
        }
    }
}
