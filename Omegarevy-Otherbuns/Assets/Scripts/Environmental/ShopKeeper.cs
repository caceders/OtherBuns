using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopKeeper : MonoBehaviour
{
    public event EventHandler OnPlayerEnterShopZone;
    public event EventHandler OnPlayerActivateShop;
    public event EventHandler OnPlayerExitShopZone;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Stats stats;
    [SerializeField] GameObject BuyPrompt;
    GameObject activeBuyPrompt;
    Vector3 buyPromptOffset = new Vector3(0, 2, 0);
    private bool playerInProximity = false;
    // Start is called before the first frame update
    void Start()
    {
        playerInput.OnInteractPress += ActivateShop;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnterShopZone?.Invoke(this, EventArgs.Empty);
            playerInProximity = true;
            activeBuyPrompt = Instantiate(BuyPrompt, this.transform.position + buyPromptOffset, Quaternion.identity);
            
        }    
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExitShopZone?.Invoke(this, EventArgs.Empty);
            stats = getBuyerStats(other.gameObject);
            playerInProximity = false;
            if(activeBuyPrompt != null)
            {
                Destroy(activeBuyPrompt);
            }
        }  
    }

    private void ActivateShop(object sender, EventArgs e)
    {
        if(playerInProximity)
        {
            BuyDamageUpgrade(stats);
        }
    }

    public void BuyDamageUpgrade(Stats stats)
    {
        if((stats.Coins.getAmount() - 100) > 0)
        {
            stats.Coins.changeAmountFlat(-100);
            stats.Strength.changeAmountFlat(1);
        }
    }
    private Stats getBuyerStats(GameObject buyer)
    {
        return buyer.GetComponent<Stats>();
    }
}
