using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGravityReverse : ActionTemplate
{
    public override float ActionID => 2;
    public override string ActionName => "Gravity";
    public override List<PatternKeys> KeyPattern
    => new List<PatternKeys>
    {
        PatternKeys.Down,
        PatternKeys.Down,
        PatternKeys.Left,
    };
    public override void ActivateAction(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale *= -1f;
        //Object needs to be rotated on y axis for sprite to be correct
        gameObject.transform.Rotate(0,180,180);
    }
}
