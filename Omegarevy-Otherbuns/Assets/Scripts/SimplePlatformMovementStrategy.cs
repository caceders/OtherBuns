using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformMovementStrategy : MonoBehaviour, IPlatformMovementStrategy
{
    public Rigidbody2D rigidBody2D{get; set;}
    public Collider2D collider2D{get; set;}

    public bool isGrounded
    {
        get{return IsCollidedInDirection(getDownDirection());}
        private set{}
    }
    public bool isFrozen { get; private set;} = false;
    public float jumpStrength{get; private set;} = 15;
    public float movementSpeed{get; private set;} = 5;
    public float xVelocity{get{return rigidBody2D.velocity.x;} private set{}}
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }
    public void MoveTowards(Vector2 movementdirection)
    {
        if(isFrozen) {rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y); return;} //Makes the character unable to move if frozen

        float VerticalVelocity = rigidBody2D.velocity.y; //Horizontal movement does not affect vertical movement;
        if(!IsCollidedInDirection(movementdirection))
        {
            rigidBody2D.velocity = new Vector2(movementdirection.x * movementSpeed, VerticalVelocity);
        }   
    }
    public void MoveTowards(GameObject objectAtDesiredPosition)
    {
        Vector2 distanceToObject = objectAtDesiredPosition.transform.position - transform.position;
        float movementDirectionX = Mathf.Abs(distanceToObject.x)/distanceToObject.x;
        float movementDirectionY = Mathf.Abs(distanceToObject.y)/distanceToObject.y;
 
        Vector2 movementDirection = new Vector2(movementDirectionX, movementDirectionY);

        MoveTowards(movementDirection);
    }
    public void Jump()
    {

        if(isFrozen) return; //Makes the character unable to move if frozen

        Vector2 downDirection = getDownDirection();
        Vector2 upDirection = - downDirection;

        //Does the actual jumping.
        if(isGrounded) rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpStrength * upDirection.y);
    }
    public void FreezeMovement()
    {
        isFrozen = true;
    }
    public void UnFreezeMovement()
    {
        isFrozen = false;
    }
    public bool IsCollidedInDirection(Vector2 Direction)
    {
        RaycastHit2D movementDirectionRayHit = Physics2D.Raycast(transform.position, Direction);
        if(!movementDirectionRayHit) return false;
        Vector2 closestPointOnColliderToOtherCollider = collider2D.ClosestPoint(movementDirectionRayHit.point);
        float distanceToCollider = (movementDirectionRayHit.point - closestPointOnColliderToOtherCollider).magnitude;

        return(distanceToCollider < 0.01f);
        
    }
    private Vector2 getDownDirection()
    {
        
        //Gets the direction of gravity to find which way is down. Reverse gravity means negative gravityscale.
        float gravityDirection = - Mathf.Abs(rigidBody2D.gravityScale)/rigidBody2D.gravityScale;
        Vector2 downDirection = new Vector2(0, gravityDirection);
        return downDirection;
    }
    public void SetMovementSpeed(float newspeed)
    {
        movementSpeed = newspeed;
    }
    public void setJumpStrength(float newJumpStrength)
    {
        jumpStrength = newJumpStrength;
    }
}
