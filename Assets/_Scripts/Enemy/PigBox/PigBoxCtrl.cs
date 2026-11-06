using UnityEngine;

public class PigBoxCtrl : SingletonPattern<PigBoxCtrl>
{
    [SerializeField] private PigBoxMovement pigBoxMovement;
    public PigBoxMovement PigBoxMovement => pigBoxMovement;
    [SerializeField] private Rigidbody2D r2d;
    public Rigidbody2D R2d => r2d;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    [SerializeField] private PigBoxAnimation pigBoxAnimation;
    public PigBoxAnimation PigBoxAnimation => pigBoxAnimation;
    [SerializeField] private ManagingSkills managingSkills;
    public ManagingSkills ManagingSkills => managingSkills;
    [SerializeField] private PigBoxHealth pigBoxHealth;
    public PigBoxHealth PigBoxHealth => pigBoxHealth;
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
        if (this.pigBoxMovement != null) return;
        this.pigBoxMovement = GetComponentInChildren<PigBoxMovement>();
        Debug.Log(transform.name + ": Load Pig Box Movement", gameObject);
    }

    protected virtual void LoadPigHealth()
    {
        if (this.pigBoxHealth != null) return;
        this.pigBoxHealth = GetComponentInChildren<PigBoxHealth>();
        Debug.Log(transform.name + ": Load Pig Box Health", gameObject);
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
        if (this.pigBoxAnimation != null) return;
        this.pigBoxAnimation = GetComponentInChildren<PigBoxAnimation>();
        Debug.Log(transform.name + ": Load Pig Animation", gameObject);
    }

    protected virtual void LoadManagingSkills()
    {
        if (this.managingSkills != null) return;
        this.managingSkills = GetComponentInChildren<ManagingSkills>();
        Debug.Log(transform.name + ": Load Pig Skills", gameObject);
    }
}
