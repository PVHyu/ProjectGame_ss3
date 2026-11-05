using UnityEngine;

public class PigBoxCtrl : SingletonPattern<PigBoxCtrl>
{
    [SerializeField] private PigBoxMovement pigThrowBoxMovement;
    public PigBoxMovement PigThrowBoxMovement => pigThrowBoxMovement;
    [SerializeField] private Rigidbody2D r2d;
    public Rigidbody2D R2d => r2d;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    [SerializeField] private PigBoxAnimation pigThrowBoxAnimation;
    public PigBoxAnimation PigThrowBoxAnimation => pigThrowBoxAnimation;
    [SerializeField] private ManagingSkills managingSkills;
    public ManagingSkills ManagingSkills => managingSkills;
    [SerializeField] private PigBoxHealth pigThrowBoxHealth;
    public PigBoxHealth PigThrowBoxHealth => pigThrowBoxHealth;
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
        if (this.pigThrowBoxMovement != null) return;
        this.pigThrowBoxMovement = GetComponentInChildren<PigBoxMovement>();
        Debug.Log(transform.name + ": Load Pig Throw Box Movement", gameObject);
    }

    protected virtual void LoadPigHealth()
    {
        if (this.pigThrowBoxHealth != null) return;
        this.pigThrowBoxHealth = GetComponentInChildren<PigBoxHealth>();
        Debug.Log(transform.name + ": Load Pig Throw Box Health", gameObject);
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this.r2d != null) return;
        this.r2d = GetComponent<Rigidbody2D>();
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
        if (this.pigThrowBoxAnimation != null) return;
        this.pigThrowBoxAnimation = GetComponentInChildren<PigBoxAnimation>();
        Debug.Log(transform.name + ": Load Pig Animation", gameObject);
    }

    protected virtual void LoadManagingSkills()
    {
        if (this.managingSkills != null) return;
        this.managingSkills = GetComponentInChildren<ManagingSkills>();
        Debug.Log(transform.name + ": Load Pig Skills", gameObject);
    }
}
