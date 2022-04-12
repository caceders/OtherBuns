using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemCollector : MonoBehaviour
{
    
    public event EventHandler OnCoinCollected;
    [SerializeField] Stats stats;

    public void increaseCoinAmount()
    {
        stats.Coins ++;
        OnCoinCollected?.Invoke(this, EventArgs.Empty);
    }

    public int getCoinAmount()
    {
        return stats.Coins;
    }
}
