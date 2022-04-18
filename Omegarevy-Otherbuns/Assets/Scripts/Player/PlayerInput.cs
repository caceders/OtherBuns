using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public event EventHandler OnInteractPress;
    public bool CheckForMovementInput()
    {
        return (Input.GetAxisRaw("Horizontal") != 0);
    }
    public float GetMovementInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }
    public bool CheckForJumpInput()
    {
        return (Input.GetButton("Jump"));
    }
    public bool CheckForInteract()
    {
        return (Input.GetKeyDown(KeyCode.E));
    }
    public bool CheckForLeft()
    {
        return (Input.GetKeyDown(KeyCode.LeftArrow));
    }
    public bool CheckForDown()
    {
        return (Input.GetKeyDown(KeyCode.DownArrow));
    }
    public bool CheckForRight()
    {
        return (Input.GetKeyDown(KeyCode.RightArrow));
    }
    public bool CheckForUp()
    {
        return (Input.GetKeyDown(KeyCode.UpArrow));
    }
    public bool CheckForKeyCodeInput()
    {
        return (CheckForLeft() | CheckForDown() | CheckForRight() | CheckForUp());
    }
    private void Update() {
        if(CheckForInteract())
        {
            OnInteractPress?.Invoke(this, EventArgs.Empty);
        }
    }

}
