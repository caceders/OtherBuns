using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlatformMovementStrategy : IMovementStrategy
{
    //Required components
    public Rigidbody2D rigidBody2D{get;}
    public Collider2D collider2D{get;}
    public bool isGrounded{get;}
    public float jumpStrength{get;}
    public float movementSpeed{get;}
    public float xVelocity{get;}
    public void Jump();
    public bool IsCollidedInDirection(Vector2 movementDirection);
}
