using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttackRight : ActionTemplate
{
    public override float ActionID => 1.1f;
    public override string ActionName => "Right attack";
    public override List<PatternKeys> KeyPattern
    => new List<PatternKeys>
    {
        PatternKeys.Right,
    };
    public override void ActivateAction(GameObject gameObject)
    {
        gameObject.GetComponent<PlayerController>().attackDirection = -1;
        gameObject.GetComponent<PlayerController>().currentState = PlayerController.PlayerState.Attacking;
    }
}
