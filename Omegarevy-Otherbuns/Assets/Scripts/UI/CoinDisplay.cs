using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text coinAmountDisplay;
    [SerializeField] private Stats stats;

    private void Start()
    {
        stats.OnStatsChange += UpdateCoinAmount;
        UpdateCoinAmount(this, EventArgs.Empty);
    }
    public void UpdateCoinAmount(object sender, EventArgs e)
    {
        coinAmountDisplay.text = ("x " + stats.Coins.getAmount().ToString());
    }
}
