using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DamageDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text damageDisplay;
    [SerializeField] private Stats stats;

    private void Start()
    {
        stats.OnStatsChange += UpdateDamage;
    }
    public void UpdateDamage(object sender, EventArgs e)
    {
        damageDisplay.text = ("COINS: " + stats.getDamage().ToString());
    }
}
