using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StrengthDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text damageDisplay;
    [SerializeField] private Stats stats;

    private void Start()
    {
        stats.OnStatsChange += UpdateDamage;
        UpdateDamage(this, EventArgs.Empty);
    }
    public void UpdateDamage(object sender, EventArgs e)
    {
        damageDisplay.text = ("x " + stats.Strength.getAmount().ToString());
    }
}
