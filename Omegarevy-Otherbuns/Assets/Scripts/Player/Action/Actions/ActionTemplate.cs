using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionTemplate
{

    public enum PatternKeys
    {
    Left = 1,
     Down = 2,
    Right = 3,
    Up = 4
    }

    public virtual float ActionID{get; private set;}
    public virtual string ActionName{get; private set;}
    public virtual List<PatternKeys> KeyPattern{get; private set;}

    public abstract void ActivateAction(GameObject gameObject);
}
