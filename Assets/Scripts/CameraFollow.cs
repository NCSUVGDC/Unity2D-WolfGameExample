using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Vector2 MinBounds;
    [SerializeField] private Vector2 MaxBounds;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition = player.transform.position + new Vector3(0, 0, -10);
        Debug.Log(newPosition);
        newPosition = new Vector3(Mathf.Clamp(newPosition.x, MinBounds.x, MaxBounds.x), Mathf.Clamp(newPosition.y, MinBounds.y, MaxBounds.y), newPosition.z);
        this.transform.position = Vector3.Lerp(this.transform.position, newPosition, .1f);
       // Vector3 Velocity = Vector3.zero;
      ////  this.transform.position = Vector3.SmoothDamp(this.transform.position, newPosition, ref Velocity, Time.deltaTime);
    }
}
