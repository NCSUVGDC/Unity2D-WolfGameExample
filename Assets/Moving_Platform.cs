using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    public float moveSpeed = 5;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime / 2;
        transform.Translate(Mathf.Sin(timer) * moveSpeed , 0, 0);
    }
}
