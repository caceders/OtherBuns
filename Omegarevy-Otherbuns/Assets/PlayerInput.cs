using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
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
}
