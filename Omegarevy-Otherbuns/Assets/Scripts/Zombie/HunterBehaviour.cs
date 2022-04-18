using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterBehaviour : MonoBehaviour
{
    [SerializeField]
    private SimplePlatformMovementStrategy hunterMovement;
    public enum HunterState
    {
        Idle,
        Hunting,
        Attacking,
        Dead
    }
    private HunterState currentHunterState = HunterState.Idle;
    private GameObject target = null;
    [SerializeField] private float hunterMovementSpeed = 2f;
    [SerializeField] private float hunterJumpStrength = 3f;
    [SerializeField] BasicDamagable damagable;
    private void Awake()
    {
        return;
    }
    private void Start()
    {
        hunterMovement.SetMovementSpeed(hunterMovementSpeed);  
        hunterMovement.setJumpStrength(hunterJumpStrength);
        hunterMovement.FreezeMovement();
        
        damagable.OnHealthMin += Die;
    }
    void Update()
    {
        switch (currentHunterState)
        {   
            case HunterState.Idle:
            hunterMovement.MoveTowards(this.gameObject);
            target = searchForTarget();
            if(target != null)
            {
                hunterMovement.UnFreezeMovement();
                currentHunterState = HunterState.Hunting;
            }
            break;

            case HunterState.Hunting:
            
            if(isSeeingTarget(target))
            {
                if(isTargetInAttackRange(target))
                {
                    currentHunterState = HunterState.Attacking;
                }
                else
                {
                    if(!HasReachedTargetXPosition(target)) hunterMovement.MoveTowards(target);
                    if(evaluateJump(target)) hunterMovement.Jump();
                }
            }
            else
            {
                currentHunterState = HunterState.Idle;
                hunterMovement.FreezeMovement();
            }
            break;

            case HunterState.Attacking:
            currentHunterState = HunterState.Hunting;
            break;

            case HunterState.Dead:
            Destroy(this.gameObject);
            break;
        }
    }

    private GameObject searchForTarget()
    {
        Collider2D[] ObjectsInRange = Physics2D.OverlapCapsuleAll
        (
            (Vector2)this.transform.position, new Vector2(15,10), CapsuleDirection2D.Horizontal, 0f
        );

        foreach (Collider2D potentialTarget in ObjectsInRange)
        {
            if(potentialTarget.gameObject.CompareTag("Player"))
            {
                return potentialTarget.gameObject;
            }
        }
        return null;
    }
    private bool isSeeingTarget(GameObject target)
    {
        return(target = searchForTarget());
    }
    private float getAngleToTarget(GameObject target)
    {
        Vector2 flatvector = new Vector2(1,0);
        Vector2 vectorToTarget = target.transform.position - transform.position;
        return Vector2.Angle(flatvector, vectorToTarget);
    }
    private float calculateAttackDistance(GameObject target)
    {
        return this.gameObject.GetComponent<Collider2D>().Distance(target.GetComponent<Collider2D>()).distance;
    }
    private bool isTargetInAttackRange(GameObject target)
    {
        return(calculateAttackDistance(target) <= .1f);
    }
    private bool evaluateJump(GameObject target)
    {
        float relativeXDistanceToTarget = target.transform.position.x - transform.position.x;
        float RelativeXToTarget = MathF.Abs(relativeXDistanceToTarget)/relativeXDistanceToTarget;
        Vector2 DirectionToTarget = new Vector2(RelativeXToTarget, 0);
        
        //Will jump if blocked
        if(hunterMovement.IsCollidedInDirection(DirectionToTarget)) return true;

        //Will jump if target is higher than 45 degrees off
        return ( 45 < getAngleToTarget(target) & getAngleToTarget(target) < 135);
    }
    private bool HasReachedTargetXPosition(GameObject target)
    {
        return (Math.Abs(target.transform.position.x - transform.position.x) <.5f);
    }    
    private void Die(object sender, EventArgs e)
    {
        currentHunterState = HunterState.Dead;
    }
}
