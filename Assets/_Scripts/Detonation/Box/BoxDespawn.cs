using System.Collections;
using UnityEngine;

public class BoxDespawn : Despawn<Detonation>
{
    [SerializeField] private EffectSpawner effectSpawner;
    [SerializeField] private Effect effect;
    [SerializeField] private BoxCtrl boxCtrl;
    [SerializeField] private float timeDelay = 0.2f;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCtrl();
    }
    
    void LoadBoxCtrl()
    {
        if(this.boxCtrl != null) return;
        this.boxCtrl = transform.parent.GetComponent<BoxCtrl>();
        Debug.Log(transform.name + ": Load BoxCtrl", gameObject);
    }

    public override void DoDespawn()
    {
        boxCtrl.Animator.SetBool("Hit", true);
        StartCoroutine(DelayDespawn());
    }

    void SpawnBoxPieces()
    {
        Effect newBoxPiece = effectSpawner.Spawn(this.effect, this.transform.position);
        newBoxPiece.gameObject.SetActive(true);
    }

    IEnumerator DelayDespawn()
    {
        yield return new WaitForSeconds(timeDelay);
        boxCtrl.Animator.SetBool("Hit", false);
        this.spawner.Despawn(this.parent);
        SpawnBoxPieces();
    }
}