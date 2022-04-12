using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopKeeper : MonoBehaviour
{
    public event EventHandler OnPlayerEnterShopZone;
    public event EventHandler OnPlayerActivateShop;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnterShopZone?.Invoke(this, EventArgs.Empty);
        }    
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Player in proximity");
        if (other.CompareTag("Player") & playerInput.CheckForInteract())
        {
            BuyDamageUpgrade(stats);
            OnPlayerActivateShop?.Invoke(this, EventArgs.Empty);
        }  
    }

    public void BuyDamageUpgrade(Stats stat)
    {
        stat.Coins -= 10;
        stat.increaseDamege(10);
    }
}
