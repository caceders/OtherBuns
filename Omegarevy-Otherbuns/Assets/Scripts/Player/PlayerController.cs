using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] SimplePlatformMovementStrategy playerMovement;
    [SerializeField] ActionManager actionManager;
    [SerializeField] BasicAttack attack;
    public int attackDirection = 0;
    public enum PlayerState
    {
        Idle,
        Attacking
    }
    public PlayerState currentState{get; set;} = PlayerState.Idle;
    
    private void Start()
    {
        if(attack == null) this.gameObject.GetComponent<BasicAttack>();
        if(attack == null) this.gameObject.AddComponent<BasicAttack>();    
    }
    private void Update()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
            actionManager.UpdateActionManager();
            break;
            case PlayerState.Attacking:

            Vector2 attackCenter = GetComponent<Collider2D>().ClosestPoint(new Vector2(attackDirection, transform.position.y));
            Vector2 attackBoxSize = (Vector2) this.transform.localScale;

            Collider2D[] ObjectsInAttackRange = Physics2D.OverlapBoxAll(attackCenter, attackBoxSize, 0);
            foreach (Collider2D collider in ObjectsInAttackRange)
            {
                IDamageable target = collider.gameObject.GetComponent<IDamageable>();
                if (!collider.gameObject.Equals(gameObject) & target != null)
                {
                    Debug.Log("Dealing damage to " + collider.name);
                    attack.Attack(target);
                }
            }

            currentState = PlayerState.Idle;
            break;
        }

    }
    private void FixedUpdate()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
            playerMovement.MoveTowards(new Vector2(playerInput.GetMovementInput(), 0));

            if (playerInput.CheckForJumpInput())
            {
                playerMovement.Jump();
            }
            break;    
            
            case PlayerState.Attacking:

            break;
        }    
    }
}
