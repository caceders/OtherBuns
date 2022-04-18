using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionInputAninmator : MonoBehaviour
{
    [SerializeField] private ActionManager actionManager;
    [SerializeField] private GameObject rightArrow;
    private List<GameObject> rightArrows = new List<GameObject>{};
    private float gridSize = 1f;
    private void Start()
    {
        actionManager.OnInputKeyCollected += UpdateAnimateInput;
        actionManager.OnInputTimeOut += ResetInputAnimation;
    }
    private void UpdateAnimateInput(object sender, EventArgs e)
    {
        MoveOldInput();
        SpawnNewInput();

    }
    private void ResetInputAnimation(object sender, EventArgs e)
    {
        foreach(GameObject arrow in rightArrows)
        {
            Destroy(arrow);
        }
        rightArrows.RemoveRange(0, rightArrows.Count);
    }
    private void MoveOldInput()
    {
        int patternLength = rightArrows.Count;
        if(patternLength == 6)
        {
            Destroy(rightArrows[0]);
            rightArrows.RemoveAt(0);
        }
        if(patternLength == 0)
        {
            return;
        }
        else
        {
            foreach (GameObject OldRightArrow in rightArrows)
            {
                Vector3 arrowRelativeToParent = OldRightArrow.transform.position - this.transform.position;
                if (arrowRelativeToParent.x <= -1.5f)
                {
                    OldRightArrow.transform.position +=
                    new Vector3(4,2,0);
                }
                else
                {
                    OldRightArrow.transform.position +=
                    new Vector3(-2,0,0);
                }
            }
        }
    }
    private void SpawnNewInput()
    {
        //finds last key
        ActionTemplate.PatternKeys key = actionManager.GetLastKey();
        Vector3 spawnposition = this.transform.position + new Vector3(2, 2, 0);
        GameObject spawnedRightArrow = Instantiate(rightArrow, spawnposition, findRotation(key));
        spawnedRightArrow.transform.SetParent(this.transform);
        rightArrows.Add(spawnedRightArrow);


    }

    private Quaternion findRotation(ActionTemplate.PatternKeys key)
    {
        Quaternion rotation = Quaternion.Euler(0,0,0);
        switch (key)
        {
            case ActionTemplate.PatternKeys.Right:
            break;
            case ActionTemplate.PatternKeys.Up:
            rotation = Quaternion.Euler(0,0,90);
            break;
            case ActionTemplate.PatternKeys.Left:
            rotation = Quaternion.Euler(0,0,180);
            break;
            case ActionTemplate.PatternKeys.Down:
            rotation = Quaternion.Euler(0,0,270);
            break;
        }
        return rotation;
    }
}
