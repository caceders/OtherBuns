using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicDamagable : MonoBehaviour, IDamageable
{
    public event EventHandler OnHealthMin;

    [SerializeField] private Stats stats;
    private void Start()
    {
        if(stats == null) stats = GetComponent<Stats>();
        if(stats == null) stats = gameObject.AddComponent<Stats>();
    }
    public void recieveDamage(IAttack attacker, float damageAmount)
    {
        Debug.Log(gameObject.name + " is recieving Damage");
        float damageAfterDefence = damageAmount - stats.Defence.getAmount();
        if(damageAfterDefence < 0) damageAfterDefence = 1;
        
        Debug.Log("Dealt this much damage: " + damageAfterDefence.ToString());

        stats.Health.changeAmountFlat(-damageAfterDefence);

        if(stats.Health.isStatAtMin())
        {
            attacker.TargetKilled(this);
            OnHealthMin.Invoke(this, EventArgs.Empty);
        }
    }
}
