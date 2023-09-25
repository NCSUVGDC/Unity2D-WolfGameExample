using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField, Tooltip("The goal objects that this platform will move between, in order. \n The first position is the position the platform will start the game moving towards")] private List<GameObject> goalObjects = new List<GameObject>();
    [SerializeField, Tooltip("the speed at which this platform will move")] private float moveSpeed = 5;
    [SerializeField, Tooltip("If marked true, this platform's path will be hidden when the game starts")] private bool hidePathOnStartup = true;
    [SerializeField, Tooltip("If marked true, this platform will snap to the first goal position when the game starts. Otherwise, it will simply move to the first position, then enter it's cycle")]private bool snapToFirstPosOnStartup = true;
    private List<Transform> goalPositions = new List<Transform>();
    //the current goal this platform is moving to
    [SerializeField]private Transform currentGoal;
    //this object's velocity in the previous frame
    private Vector2 vel = Vector2.zero;
    //the index of the current goal
    private int currentGoalIndex;
    private int prevGoalListLength = -1;
    [SerializeField, Tooltip("the parent transform to set as parent of riding objects. \n by default, will be this transform")]private Transform parentTransform;
    [Header("DO NOT Change these fields")]
    [SerializeField, Tooltip("the holder for all goal positions")]private Transform goalsHolder;
    [SerializeField, Tooltip("the goal position prefab")]private GameObject goalPosPrefab;
    
    //private LineRenderer startMoveLine;
  
    // Start is called before the first frame update
    void Start()
    {
        if(!parentTransform){
            parentTransform = transform;
        }
        if(goalObjects == null || goalObjects.Count == 0)
        {
            Debug.LogWarning("NO GOAL POSITIONS HAVE BEEN SET");
        }
        else
        {
            Debug.Log(goalObjects.Count);
            goalPositions = new List<Transform>();
            for (int i = 0; i < goalObjects.Count; i++)
            {
                GameObject go = goalObjects[i];

                goalPositions.Add(go.transform);
            }
            
            currentGoal = goalPositions[0]; // Getting error in build
            currentGoalIndex = 0;
            if(snapToFirstPosOnStartup){
                transform.position = currentGoal.position;
            }
            if (hidePathOnStartup)
            {
                foreach (GameObject go in goalObjects)
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
        Vector3 oldPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, currentGoal.position, moveSpeed * Time.fixedDeltaTime);
        Vector3 vel3D = transform.position - oldPos;
        vel = new Vector2(transform.position.x - oldPos.x, transform.position.y - oldPos.y) / Time.fixedDeltaTime;
    }

    /// <summary>
    /// Returns this moving platform's velocity
    /// </summary>
    /// <returns>the distance this platform moved this past frame</returns>
    public Vector2 GetVel(){
        return vel;
    }

    /// <summary>
    /// Gets the parent transform to set as child of objects riding this platform
    /// </summary>
    /// <returns></returns>
    public Transform GetParentTransform(){
        return parentTransform;
    }

    

//          -----------------------------------------------------------------------------------------------------------------------------------
//          ---If you are viewing this as part of the VGDC into to unity exercises, 
//          ---we strongly suggest you avoid any parts of the script below this point unless you are very familiar with Unity and C# scripting
//          ---This part of the script is somewhat complicated, and involves easily breakable code.
//          ---edit at your own risk
//          -----------------------------------------------------------------------------------------------------------------------------------


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
#if UNITY_EDITOR
    public void AddNewGoal(){
        if(!goalPosPrefab){
            Debug.LogError("Can't add goal pos because no goal position prefab is connected.\nPlease drag the prefab into the goalPosPrefab field");
        }
        if(!goalsHolder){
            goalsHolder = transform.parent.GetChild(1);
        }
        GameObject obj = Instantiate(goalPosPrefab, goalsHolder);
        Undo.RegisterCreatedObjectUndo(obj, "Create GameObject");
        goalObjects.Add(obj);
        //OnValidate();
        EditorUtility.SetDirty(this);
    }
#endif

}
