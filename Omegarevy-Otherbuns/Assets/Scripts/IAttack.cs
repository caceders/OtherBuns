using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAttack
{
    public event EventHandler onAttack;
    public float baseDamage{get;}
    public float baseKnockBack{get;}
    public void Attack(IDamageable target);
    public void TargetKilled(IDamageable target);
}
