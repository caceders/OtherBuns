using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMovementStrategy
{
    public event EventHandler OnEntityStartMovement;
    public event EventHandler OnEntityStopMovement;
    public enum HorizontalMovementDirection
    {
        Still = 0,
        Left = 1,
        Right = 2
    }
    public enum VerticalMovementDirection
    {
        Still = 0,
        Up = 1,
        Down = 2
    }
    public struct MovementDirection
    {
        public HorizontalMovementDirection horizontalMovementDirection;
        public VerticalMovementDirection verticalMovementDirection;
    }
    public float movementSpeed{get;}
    public bool isFrozen{get;}
    public bool isGrounded{get;}

    public MovementDirection GetMovementDirection();
    public void MoveTowards(Vector2 direction);
    public void MoveTowards(GameObject objectAtDesiredPosition);
    public void FreezeMovement();
    public void UnFreezeMovement();
    public float getXVelocity();
    public float getYVelocity();
}
