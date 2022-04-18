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
        stats.Coins.changeAmountFlat(1);
        OnCoinCollected?.Invoke(this, EventArgs.Empty);
    }
}
