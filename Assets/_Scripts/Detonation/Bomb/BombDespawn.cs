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

    public override void DoDespawn()
    {
        bombCtrl.Animator.SetTrigger("BombOn");
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
        this.spawner.Despawn(this.parent);
        SpawnExplosion();
    }
}