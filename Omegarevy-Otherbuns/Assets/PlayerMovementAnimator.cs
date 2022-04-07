using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnimator : MonoBehaviour
{
    [SerializeField] private Animator playerMovementAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerMovement playerMovement;
    void Start()
    {
        
    }

    void Update()
    {
        spriteRenderer.flipX = !playerMovement.GetisFacingRigth();

        switch (playerMovement.getPlayerMovementState())
        {
            case PlayerMovement.PlayerMovementState.Grounded:
                playerMovementAnimator.SetBool("isWalking", playerMovement.GetIsWalking());
                break;
            case PlayerMovement.PlayerMovementState.Jumping:
                playerMovementAnimator.SetBool("isWalking", false);
                break;
            default:
                break;
        }
    }
}
