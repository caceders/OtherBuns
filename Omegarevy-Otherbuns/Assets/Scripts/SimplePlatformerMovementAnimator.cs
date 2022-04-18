using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformerMovementAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SimplePlatformMovementStrategy movement;
    private bool isFacingLeft = false;
    void Update()
    {
        animator.speed = movement.movementSpeed/5; //Standard movementspeed is 5
        spriteRenderer.flipX  = checkIfFacingLeft();
        if(Mathf.Abs(movement.xVelocity) > .4f & movement.isGrounded) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);
    }
    private bool checkIfFacingLeft()
    {
        if(movement.xVelocity < 0) return true;
        else if(movement.xVelocity > 0) return false;
        else return isFacingLeft;
    }
}
