using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    int direction;
    bool moving;

    private void Start()
    {
        direction = 1;
        moving = false;
    }

    public void SetDirection(int direction)
    { 
        this.direction = direction; 
    }

    public void Go()
    {
        moving = true;
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            transform.position = new Vector3(transform.position.x + direction * 0.1f, transform.position.y, transform.position.z); 
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2);

        Destroy(this);
    }
}
