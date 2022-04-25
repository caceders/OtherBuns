using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicDamagable : MonoBehaviour, IDamageable
{
    public event EventHandler OnHealthMin;
    public event EventHandler onDamageRecieved;

    [SerializeField] private Stats stats;
    private void Start()
    {
        if(stats == null) stats = gameObject.GetComponent<Stats>();
        if(stats == null) stats = gameObject.AddComponent<Stats>();
    }
    public void recieveDamage(IAttack attacker, float damageAmount)
    {
        float damageAfterDefence = damageAmount - stats.Defence.getAmount();
        if(damageAfterDefence < 0) damageAfterDefence = 1;
        
        stats.Health.changeAmountFlat(-damageAfterDefence);

        onDamageRecieved?.Invoke(this, EventArgs.Empty);

        if(stats.Health.isStatAtMin())
        {
            attacker.TargetKilled(this);
            OnHealthMin?.Invoke(this, EventArgs.Empty);
        }
    }
}
