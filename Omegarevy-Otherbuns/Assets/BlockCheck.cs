using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheck : MonoBehaviour
{
    [SerializeField] private Collider2D BlockCheckCollider;
    
    public event EventHandler OnBlockCollideEnter;
    public event EventHandler OnBlockCollideExit;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blocking"))
        {
            OnBlockCollideEnter?.Invoke(this, EventArgs.Empty);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Blocking"))
        {
            OnBlockCollideExit?.Invoke(this, EventArgs.Empty);
        }
    }
}
