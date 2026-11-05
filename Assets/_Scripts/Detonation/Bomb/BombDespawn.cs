using System.Collections;
using UnityEngine;

public class BombDespawn : Despawn<Detonation>
{
    [SerializeField] private EffectSpawner effectSpawner;
    [SerializeField] private Effect effect;
    [SerializeField] private BombCtrl bombCtrl;
    [SerializeField] private float timeDelay = 0.2f;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBombCtrl();
    }
    
    void LoadBombCtrl()
    {
        if(this.bombCtrl != null) return;
        this.bombCtrl = transform.parent.GetComponent<BombCtrl>();
        Debug.Log(transform.name + ": Load BombCtrl", gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) return; 
        Debug.Log("Collision with: " + collision.gameObject.name);
        DoDespawn();
    }

    public override void DoDespawn()
    {
        bombCtrl.Animator.SetBool("BombOn", true);
        StartCoroutine(DelayDespawn());
    }

    void SpawnExplosion()
    {
        Effect newExplosion = effectSpawner.Spawn(this.effect, this.transform.position);
        newExplosion.gameObject.SetActive(true);
    }

    IEnumerator DelayDespawn()
    {  
        yield return new WaitForSeconds(timeDelay);
        bombCtrl.Animator.SetBool("BombOn", false);  
        this.spawner.Despawn(this.parent);
        SpawnExplosion();
    }
}