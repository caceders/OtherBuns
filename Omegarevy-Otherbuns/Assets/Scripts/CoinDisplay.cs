using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text coinAmountDisplay;
    [SerializeField] private ItemCollector itemCollector;

    private void Start()
    {
        itemCollector.OnCoinCollected += UpdateCoinAmount;
    }
    public void UpdateCoinAmount(object sender, EventArgs e)
    {
        coinAmountDisplay.text = ("COINS: " + itemCollector.getCoinAmount().ToString());
    }
}
