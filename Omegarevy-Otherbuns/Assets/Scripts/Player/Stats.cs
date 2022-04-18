using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats : MonoBehaviour
{
    public event EventHandler OnStatsChange;
    public Stat Health;
    public Stat Strength;
    public Stat Defence;
    public Stat Coins;
    public List<Stat> StatsList;
    private void Awake()
    {
        if (Health == null) {Health = new Stat(); Health.CopyStat(BaseStats.BaseHealth);};
        if (Strength == null) {Strength = new Stat(); Strength.CopyStat(BaseStats.BaseStrength);};
        if (Defence == null) {Defence = new Stat(); Defence.CopyStat(BaseStats.BaseDefence);};
        if (Coins == null) {Coins = new Stat(); Coins.CopyStat(BaseStats.BaseCoins);};

        StatsList = new List<Stat>{
            Health, Strength, Defence, Coins
        };

        foreach (Stat stat in StatsList)
        {
            //Notice the difference between OnStatChage and OnStatsChange.
            stat.OnStatChange += SendOnStatsChange;
        }
    }
    private void SendOnStatsChange(object sender, EventArgs e)
    {
        OnStatsChange?.Invoke(this, EventArgs.Empty);
    }
}
public static class BaseStats
{
    public static Stat BaseHealth = new Stat("Health", 10, 0, 10);
    public static Stat BaseStrength = new Stat("Strength", initialAmount:10);
    public static Stat BaseDefence = new Stat("Defence", initialAmount:1);
    public static Stat BaseCoins = new Stat("Coins");
}
public class Stat
{
    public event EventHandler OnStatChange;

    private string __statName;
    private string _statName{get{return __statName;} set{__statName = value; SendOnStatChange();}}
    private float __maxAmount;
    private float _maxAmount{get{return __maxAmount;} set{__maxAmount = value; SendOnStatChange();}}
    private float __minAmount;    
    private float _minAmount{get{return __minAmount;} set{__minAmount = value; SendOnStatChange();}}
    private float __amount;
    private float _amount{get{return __amount;} set{__amount = value; SendOnStatChange();}}
   
    public Stat(string statName = "stat", float max = float.MaxValue, float min = 0, float initialAmount = 0)
   {_statName = statName; _maxAmount = max; _minAmount = min; _amount = initialAmount;}

   public Stat(Stat stat)
   {_statName = stat._statName; _maxAmount = stat._maxAmount; _minAmount = stat._minAmount; _amount = stat._amount;}
    public float getAmount()
   {
       return _amount;
   }
    public void changeAmountFlat(float flatAmountChange)
   {
       _amount += flatAmountChange;

       if(_amount > _maxAmount) _amount = _maxAmount;
       else if (_amount < _minAmount) _amount = _minAmount;
   }
    public void changeAmountPercent(float percent)
    {
        _amount += percent * _amount;
    }
    public float getMaxAmount()
    {
        return _maxAmount;
    }
    public void changeMaxAmountFlat(float flatAmountChange)
    {
        _maxAmount += flatAmountChange;
    }
    public void changeMaxAmountPercent(float percent)
    {
       _maxAmount += _maxAmount * percent;
    }
    public float getminAmount()
    {
        return _minAmount;
    }
    public void changeMinAmountFlat(float flatAmountChange)
    {
        _minAmount += flatAmountChange;
    }
    public void changeMinAmountPercent(float percent)
    {
        _minAmount += percent * _minAmount;
    }
    
    public bool isStatAtMax()
    {
        return (_amount == _maxAmount);
    }
    public bool isStatAtMin()
    {
        return (_amount == _minAmount);
    }
    //Returns the percentage the amount has "filled" of the max amount or min amount
    public float PercentOfFilledMax()
    {
        return _amount/_maxAmount;
    }
    public float PercentOfFilledMin()
    {
        return _amount/_minAmount;
    }
    
    public string GetStatName()
    {
        return _statName;
    }
    private void SendOnStatChange()
    {
        OnStatChange?.Invoke(this, EventArgs.Empty);
    }
    public void CopyStat(Stat stat)
    {
        this._statName = stat.GetStatName();
        this._amount = stat.getAmount();
        this._maxAmount = stat.getMaxAmount();
        this._minAmount = stat.getminAmount();
    }
}
