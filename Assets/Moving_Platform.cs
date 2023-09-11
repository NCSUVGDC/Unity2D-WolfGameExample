using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField, Tooltip("The goal objects that this platform will move between, in order. \n The first position is the position the platform will start the game moving towards")] private List<Transform> goalPositions = new List<Transform>();
    [SerializeField, Tooltip("the speed at which this platform will move")] private float moveSpeed = 5;
    //the current goal this platform is moving to
    private Transform currentGoal;
    //the index of the current goal
    private int currentGoalIndex;
    // Start is called before the first frame update
    void Start()
    {
        if(goalPositions == null || goalPositions.Count == 0)
        {
            Debug.LogWarning("NO GOAL POSITIONS HAVE BEEN SET");
        }
        else
        {
            currentGoal = goalPositions[0];
            currentGoalIndex = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position == currentGoal.position)
        {
            if(currentGoalIndex >= goalPositions.Count - 1)
            {
                currentGoalIndex = 0;
            }
            else
            {
                currentGoalIndex++;
            }
            currentGoal = goalPositions[currentGoalIndex];
        }
        transform.position = Vector3.MoveTowards(transform.position, currentGoal.position, moveSpeed * Time.fixedDeltaTime);
    }
}
