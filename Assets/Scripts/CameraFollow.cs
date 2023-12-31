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
        if (player != null)
        {
            Vector3 newPosition = player.transform.position + new Vector3(0, .5f, -10);
            newPosition = new Vector3(Mathf.Clamp(newPosition.x, MinBounds.x, MaxBounds.x), Mathf.Clamp(newPosition.y, MinBounds.y, MaxBounds.y), newPosition.z);
            this.transform.position = Vector3.Lerp(this.transform.position, newPosition, .1f);
        }
    }
}
