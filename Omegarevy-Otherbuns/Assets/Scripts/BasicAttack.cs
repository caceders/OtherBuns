using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour, IAttack
{
    [SerializeField] Stats stats;

    public event EventHandler onAttack;
    public event EventHandler onTargetKilled;

    public float baseDamage => 0;

    public float baseKnockBack => 0;
    private void Start()
    {
        if(stats == null) stats = GetComponent<Stats>();
        if(stats == null) stats = gameObject.AddComponent<Stats>();
    }

    public void Attack(IDamageable target)
    {
        onAttack?.Invoke(this, EventArgs.Empty);
        if(target!=null) target.recieveDamage(this, stats.Strength.getAmount());
    }

    public void TargetKilled(IDamageable target)
    {
        onTargetKilled?.Invoke(this, EventArgs.Empty);
        return;
    }
}
