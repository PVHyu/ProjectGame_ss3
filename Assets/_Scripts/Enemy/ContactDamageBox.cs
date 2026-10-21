using UnityEngine;
using System.Collections.Generic;

public class ContactDamageBox : DamageSender
{
    [SerializeField] private float attackDuration = 1f;

    private float attackTimer = 0f;
    private DamageReceiver damageReceiver;

    private readonly HashSet<PlayerHealth> playersInRange = new();

    private void FixedUpdate()
    {
        Attacking();
    }

    protected virtual void Attacking()
    {
        if (playersInRange.Count == 0) return;       
        attackTimer += Time.fixedDeltaTime;

        if (attackTimer < attackDuration) return;
        attackTimer = 0f;

        foreach (PlayerHealth player in playersInRange)
        {
            if (player == null) continue;
            if(damageReceiver == null) return;
            this.Send(damageReceiver);
        }
    }

    public void BeginAttack()
    {
        if (this.collider != null)
            this.collider.enabled = true;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "PigDamageReceiver") return;

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player == null) return;
        playersInRange.Add(player);

        damageReceiver = other.GetComponent<DamageReceiver>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player == null) return;
        playersInRange.Remove(player);
    }
}