using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField, Tooltip("The goal objects that this platform will move between, in order. \n The first position is the position the platform will start the game moving towards")] private List<GameObject> goalObjects = new List<GameObject>();
    [SerializeField, Tooltip("the speed at which this platform will move")] private float moveSpeed = 5;
    [SerializeField, Tooltip("If marked true, this platform's path will be hidden when the game starts")] private bool hidePathOnStartup = true;
    private List<Transform> goalPositions = new List<Transform>();
    //the current goal this platform is moving to
    [SerializeField]private Transform currentGoal;
    //the index of the current goal
    private int currentGoalIndex;
    private int prevGoalListLength = -1;
    // Start is called before the first frame update
    void Start()
    {
        if(goalObjects == null || goalObjects.Count == 0)
        {
            Debug.LogWarning("NO GOAL POSITIONS HAVE BEEN SET");
        }
        else
        {
            
            currentGoal = goalPositions[0];
            currentGoalIndex = 0;
            if (hidePathOnStartup)
            {
                foreach(GameObject go in goalObjects)
                {
                    go.GetComponent<GoalPosVisual>().HideRenderers();
                }
            }

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Moving");
        if(Vector3.Distance(transform.position, currentGoal.position) < .1)
        {
            //Debug.Log("At goal");
            currentGoalIndex++;
            if(currentGoalIndex > goalPositions.Count - 1)
            {
                currentGoalIndex = 0;
            }
            currentGoal = goalPositions[currentGoalIndex];
        }
        transform.position = Vector3.MoveTowards(transform.position, currentGoal.position, moveSpeed * Time.fixedDeltaTime);
    }



    /// <summary>
    /// Update the line path whenever the goal positions are updated
    /// </summary>
    public void UpdatePositions()
    {
        if(prevGoalListLength > goalObjects.Count)
        {
            for(int i = goalObjects.Count - 1; i > 0; i--)
            {
                if (goalObjects[i] == null)
                {
                    goalObjects.RemoveAt(i);
                    
                }
            }   
        }
        goalPositions = new List<Transform>();
        //foreach (GameObject go in goalObjects)
        //{
        //    GoalPosVisual gpv = go.GetComponent<GoalPosVisual>();
        //    if (!gpv)
        //    {
        //        go.AddComponent<GoalPosVisual>();
        //    }
        //    gpv.SetMovingObject(this);
        //    goalPositions.Add(go.transform);
        //}
        for (int i = 0; i < goalObjects.Count - 1; i++)
        {
            GameObject go = goalObjects[i];
            if (go == null)
            {
                goalObjects.RemoveAt(i);
                i--;
                continue;
            }
            while (i < goalObjects.Count - 1 && goalObjects[i+1] == null)
            {
                goalObjects.RemoveAt(i + 1);
            }
            if(i >= goalObjects.Count - 1)
            {
                break;
            } 
            GoalPosVisual gpv = go.GetComponent<GoalPosVisual>();
            if (!gpv)
            {
                go.AddComponent<GoalPosVisual>();
            }
            gpv.SetMovingObject(this);
            goalPositions.Add(go.transform);
            LineRenderer line = go.GetComponent<LineRenderer>();
            if (!line)
            {
                go.AddComponent<LineRenderer>();
            }
            line.startWidth = .1f;
            line.endWidth = .1f;
            line.SetPositions(new Vector3[] { go.transform.position, goalObjects[i + 1].transform.position });
        }
        if (goalObjects.Count > 1)
        {
            GameObject go = goalObjects[goalObjects.Count - 1];
            if (!go)
            {
                goalObjects.RemoveAt(goalObjects.Count - 1);
                return;
            }
            goalPositions.Add(go.transform);
            LineRenderer lastLine = go.GetComponent<LineRenderer>();
            if (!lastLine)
            {
                go.AddComponent<LineRenderer>();
            }
            lastLine.startWidth = .1f;
            lastLine.endWidth = .1f;
            lastLine.SetPositions(new Vector3[] { go.transform.position, goalObjects[0].transform.position });
        }
    }

    private void OnValidate()
    {
        goalObjects.RemoveAll(s => s == null);
        if (prevGoalListLength < 0)
        {
            prevGoalListLength = goalObjects.Count;
        }
        //update lines
        UpdatePositions();
        prevGoalListLength = goalObjects.Count;
    }
}
