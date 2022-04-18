using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttackLeft : ActionTemplate
{
    public override float ActionID => 1.2f;
    public override string ActionName => "Left attack";
    public override List<PatternKeys> KeyPattern
    => new List<PatternKeys>
    {
        PatternKeys.Left,
    };
    public override void ActivateAction(GameObject gameObject)
    {
        gameObject.GetComponent<PlayerController>().attackDirection = -1;
        gameObject.GetComponent<PlayerController>().currentState = PlayerController.PlayerState.Attacking;
    }
}
