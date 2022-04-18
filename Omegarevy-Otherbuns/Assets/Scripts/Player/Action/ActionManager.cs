using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ActionManager : MonoBehaviour
{
    //Events
    public event EventHandler OnInputKeyCollected;
    public event EventHandler OnInputTimeOut;
    //Refrences
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerInput playerInput;
    private float timeUntilInputTimeOut = 0.3f;
    private float timeAtInputTimeOut = 0f;
    //Available Actions
    private ActionTemplate actionEmpty = new ActionEmpty();
    private ActionTemplate actionAttackLeft = new ActionAttackLeft();
    private ActionTemplate actionAttackRight = new ActionAttackRight();
    private ActionTemplate actionGravityReverse = new ActionGravityReverse();
    private ActionTemplate actionDoubleSize = new ActionDoubleSize();
    private ActionTemplate actionHalfSize = new ActionHalfSize();
    public List<ActionTemplate.PatternKeys> keyPattern
    = new List<ActionTemplate.PatternKeys>{};
    private Dictionary<List<ActionTemplate.PatternKeys>, ActionTemplate> KeyPatternDictionary
    = new Dictionary<List<ActionTemplate.PatternKeys>, ActionTemplate>{};
    public enum ActionManagerState
    {
        Waiting,
        Collecting,
    }
    private ActionManagerState state = ActionManagerState.Waiting;
    
        void Awake()
    {
        KeyPatternDictionary.Add(actionEmpty.KeyPattern, actionEmpty);
        KeyPatternDictionary.Add(actionAttackRight.KeyPattern, actionAttackRight);
        KeyPatternDictionary.Add(actionAttackLeft.KeyPattern, actionAttackLeft);
        KeyPatternDictionary.Add(actionGravityReverse.KeyPattern, actionGravityReverse);
        KeyPatternDictionary.Add(actionDoubleSize.KeyPattern, actionDoubleSize);
        KeyPatternDictionary.Add(actionHalfSize.KeyPattern, actionHalfSize);
    }
    public void UpdateActionManager()
    {
        switch (state )
        {
            case ActionManagerState.Waiting:
            WaitForInput();
            break;
            case ActionManagerState.Collecting:
            CollectInput();
            break;
        }
    }
    private void UpdateInputTimeOutTimer()
    {
        timeAtInputTimeOut = Time.time + timeUntilInputTimeOut;
    }
    private void CollectKeyPattern()
    {
        if (playerInput.CheckForLeft())
        {
            keyPattern.Add(ActionTemplate.PatternKeys.Left);
        }
        if (playerInput.CheckForDown())
        {
            keyPattern.Add(ActionTemplate.PatternKeys.Down);
        }
        if (playerInput.CheckForRight())
        {
            keyPattern.Add(ActionTemplate.PatternKeys.Right);
        }
        if (playerInput.CheckForUp())
        {
            keyPattern.Add(ActionTemplate.PatternKeys.Up);
        }
        //Needs to be at bottom to be fired AFTER key is collected.
        if (playerInput.CheckForKeyCodeInput())
        {
            //removes the fist keyinout to limit combination to 6
            if(keyPattern.Count == 7)
            {
                keyPattern.RemoveAt(0);
            }
            OnInputKeyCollected?.Invoke(this, EventArgs.Empty);
        }
    }
    public ActionTemplate DecodeKeyPattern(List<ActionTemplate.PatternKeys> keyPattern)
    {
        foreach(List<ActionTemplate.PatternKeys> ActionKeyPattern in KeyPatternDictionary.Keys)
        {
            if(ActionKeyPattern.SequenceEqual(keyPattern))
            {
                return KeyPatternDictionary[ActionKeyPattern];
            }
        }

        //Returns empty action if no action matching the key was found
        return actionEmpty;
    }
    private void WaitForInput()
    {
        if(playerInput.CheckForKeyCodeInput())
        {
            CollectKeyPattern();
            state = ActionManagerState.Collecting;
            UpdateInputTimeOutTimer();
        }
    }
    private void CollectInput()
    {
        if(Time.time > timeAtInputTimeOut)
        {
            OnInputTimeOut?.Invoke(this, EventArgs.Empty);
            DecodeKeyPattern(keyPattern).ActivateAction(player);
            state = ActionManagerState.Waiting;
            keyPattern = new List<ActionTemplate.PatternKeys>{};
            return;
        }
        else
        {
            if(playerInput.CheckForKeyCodeInput())
            {
                UpdateInputTimeOutTimer();
                CollectKeyPattern();
            }
        }
    }
    public ActionManagerState getState()
    {
        return state;
    }
    public List<ActionTemplate.PatternKeys> GetKeyPattern()
    {
        return keyPattern;
    }
    public ActionTemplate.PatternKeys GetLastKey()
    {
        return keyPattern.Last();
    }
}
