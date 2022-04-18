using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHalfSize : ActionTemplate
{
    public override float ActionID => 4;
    public override string ActionName => "Half Size";
    public override List<PatternKeys> KeyPattern
    => new List<PatternKeys>
    {
        PatternKeys.Down,
        PatternKeys.Down,
        PatternKeys.Down,
        PatternKeys.Down,
        PatternKeys.Down,
        PatternKeys.Down
    };
    public override void ActivateAction(GameObject gameObject)
    {
        gameObject.transform.localScale /= 2f;
    }
}
