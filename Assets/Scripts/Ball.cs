using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    int direction = 1;
    bool moving = false;

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
        {
            this.gameObject.transform.position = new Vector3(transform.position.x + direction * 7 * Time.deltaTime, transform.position.y, transform.position.z);
            //Debug.Log(transform.position);

        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5);

        Destroy(this.gameObject);
    }
}
