using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDoubleSize : ActionTemplate
{
    public override float ActionID => 3;
    public override string ActionName => "Double Size";
    public override List<PatternKeys> KeyPattern
    => new List<PatternKeys>
    {
        PatternKeys.Up,
        PatternKeys.Up,
        PatternKeys.Up,
        PatternKeys.Up,
        PatternKeys.Up,
        PatternKeys.Up
    };
    public override void ActivateAction(GameObject gameObject)
    {
        gameObject.transform.localScale *= 2f;
    }
}
