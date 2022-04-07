using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PlayerMovement: MonoBehaviour
{
    //Refrences
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInput pi;
    [SerializeField] private BlockCheck groundCheck;
    [SerializeField] private BlockCheck roofCheck;
    [SerializeField] private BlockCheck leftCollidedCheck;
    [SerializeField] private BlockCheck rightCollidedCheck;

    //States
    public enum PlayerMovementState
    {
        Grounded,
        Jumping,
    }

    PlayerMovementState currentMovementState;
    [SerializeField] private float movementspeed;
    [SerializeField] private float jumpstrength;
    [SerializeField] private float jumpSmoothing = 0.1f;
    [SerializeField] private float jumpCoolDownTime = .1f;

    private float JumpHasCooledDownTime = 0f;
    private float jumpForce = 1f;
    private bool isWalking = false;
    private bool isFacingRigth = true;

    private bool isGrounded;
    private bool isRoofed;
    private bool isLeftCollided;
    private bool isRightCollided;
    void Start()
    {

        groundCheck.OnBlockCollideEnter += OnPlayerLandEnter;
        groundCheck.OnBlockCollideExit += OnPlayerAerialEnter;

        roofCheck.OnBlockCollideEnter += OnRoofCollideEnter;
        roofCheck.OnBlockCollideExit += OnRoofCollideExit;

        leftCollidedCheck.OnBlockCollideEnter += OnLeftCollideEnter;
        leftCollidedCheck.OnBlockCollideExit += OnLeftCollideExit;

        rightCollidedCheck.OnBlockCollideEnter += OnRightCollideEnter;
        rightCollidedCheck.OnBlockCollideExit += OnRightCollideExit;

        currentMovementState = PlayerMovementState.Grounded;
    }
    void FixedUpdate()
    {
        HandlePlayerMovement();
        switch (currentMovementState)
        {
            case PlayerMovementState.Grounded:
                HandlePlayerJump();
                break;
            case PlayerMovementState.Jumping:
                SmoothJump();
                if (isGrounded)
                {
                    Switchstate(PlayerMovementState.Grounded);
                }
                break;
            default:
                break;
        }
    }
    private void OnDestroy()
    {
        groundCheck.OnBlockCollideEnter -= OnPlayerLandEnter;
    }

    void HandlePlayerMovement()
    {
        //Sets the movement direction
        float movementDirection = pi.GetMovementInput();
        SetisFacingRigth(movementDirection);
        bool tryingToMoveRight = movementDirection > 0;
        bool tryingToMoveLeft = movementDirection < 0;
        if(movementDirection != 0)
        {
            if (!(tryingToMoveRight & isRightCollided) & !(tryingToMoveLeft & isLeftCollided))
            {
                isWalking = true;
                rb.velocity = new Vector2(movementDirection * movementspeed, rb.velocity.y);
            }
        }
        else
        {
            isWalking = false;
        }
    }
    void HandlePlayerJump()
    {
        if (pi.CheckForJumpInput() & !isRoofed & JumpHasCooledDownTime < Time.time)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpstrength);
        }
    }
    //makes the player jump higher if jump is pressed for longer
    void SmoothJump()
    {
        if (jumpForce > 0 & rb.velocity.y > 0)
        {
            if (pi.CheckForJumpInput())
            {

            }
            else
            {
                jumpForce -= jumpSmoothing;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpForce);
            }
        }

    }
    void OnPlayerLandEnter(object sender, EventArgs e)
    {
        JumpHasCooledDownTime = Time.time + jumpCoolDownTime;
        Switchstate(PlayerMovementState.Grounded);
        jumpForce = 1;
    }
    void OnPlayerAerialEnter(object sender, EventArgs e)
    {
        isWalking = false;
        Switchstate(PlayerMovementState.Jumping);
    }
    void OnRoofCollideEnter(object sender, EventArgs e)
    {
        isRoofed = true;
    }
    void OnRoofCollideExit(object sender, EventArgs e)
    {
        isRoofed = false;
    }
    void OnLeftCollideEnter(object sender, EventArgs e)
    {
        isLeftCollided = true;
    }
    void OnLeftCollideExit(object sender, EventArgs e)
    {
        isLeftCollided = false;
    }
    void OnRightCollideEnter(object sender, EventArgs e)
    {
        isRightCollided = true;
    }
    void OnRightCollideExit(object sender, EventArgs e)
    {
        isRightCollided = false;
    }
    void Switchstate(PlayerMovementState state)
    {
        currentMovementState = state;
    }
    public PlayerMovementState getPlayerMovementState()
    {
        return currentMovementState;
    }
    public bool GetIsWalking()
    {
        return isWalking;
    }
    public void SetisFacingRigth(float movementDirection)
    {
        if(movementDirection != 0)
        {
            isFacingRigth = (movementDirection > 0);
        }
    }
    public bool GetisFacingRigth()
    {
        return isFacingRigth;
    }
}
