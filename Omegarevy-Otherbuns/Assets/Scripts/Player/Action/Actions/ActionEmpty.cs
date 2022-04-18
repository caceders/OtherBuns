using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEmpty : ActionTemplate
{
    public override float ActionID => 0;
    public override string ActionName => "Empty";
    public override List<PatternKeys> KeyPattern
    => new List<PatternKeys>{};
    public override void ActivateAction(GameObject gameObject)
    {
        
        return;
    }
}
