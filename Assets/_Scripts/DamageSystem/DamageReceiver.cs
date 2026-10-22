using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : BaseMonoBehaviour
{
    public int MaxHP {get; set;} = 100;
    public int CurrentHP {get; set;} = 100;
    protected bool isDead = false;
    
    [SerializeField] protected bool isImmotal = false;

    protected override void OnEnable()
    {
        this.OnReborn();
    }

    public virtual int Deduct(int hp)
    {
        this.CurrentHP -= hp;
        if(this.IsDead()) 
        {
            this.OnDead();
        }
        else
        {
            this.OnHurt();
        }
        if(this.CurrentHP <= 0) this.CurrentHP = 0;
        return this.CurrentHP;
    }

    public virtual bool IsDead()
    {
        Debug.Log(transform.name + ": " + CurrentHP + " / " + MaxHP);
        return this.isDead = this.CurrentHP <= 0;
    }

    protected virtual void OnDead()
    {
        //For Override
    }

    protected virtual void OnHurt()
    {
        //For Override
    }

    protected virtual void OnReborn()
    {
        this.CurrentHP = this.MaxHP;
        // this.isDead = false;
    }
}