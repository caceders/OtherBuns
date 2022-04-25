using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamageable
{
    public event EventHandler onDamageRecieved;
    public event EventHandler OnHealthMin;
    public void recieveDamage(IAttack attacker, float damageAmount);
}
