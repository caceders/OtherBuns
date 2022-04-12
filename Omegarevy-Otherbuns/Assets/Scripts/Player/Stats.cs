using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats : MonoBehaviour
{
    public event EventHandler OnStatsChange;
    public int Damage = 0;
    public int Coins = 0;

    public int getDamage()
    {
        return Damage;
    }
    public void increaseDamege(int amount)
    {
        Damage += amount;
        OnStatsChange?.Invoke(this, EventArgs.Empty);
    }
}
