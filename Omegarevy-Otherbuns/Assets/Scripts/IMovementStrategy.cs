using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementStrategy
{
    public bool isFrozen{get;}

    public void MoveTowards(Vector2 direction);
    public void MoveTowards(GameObject objectAtDesiredPosition);
    public void FreezeMovement();
    public void UnFreezeMovement();
}
