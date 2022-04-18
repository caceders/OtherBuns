using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamageable
{
    public void recieveDamage(IAttack attacker, float damageAmount);
}
