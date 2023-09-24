using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresher : MonoBehaviour
{
    [SerializeField] private float RegenerateTime = 3.0f;

    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    
    public void Collect()
    {
        timer = RegenerateTime;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
