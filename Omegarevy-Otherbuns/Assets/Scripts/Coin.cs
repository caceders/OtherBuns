using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.CompareTag("Player"))
        {
            collider.GetComponent<ItemCollector>().increaseCoinAmount();
            Destroy(this.gameObject);
        }
    }
}
