using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GoalPosVisual : MonoBehaviour
{
    [SerializeField, Tooltip("the moving platform that is going to move through this position")]
    Moving_Platform movingObject;
    private Renderer[] renderers;
    // Start is called before the first frame update
    void Awake()
    {
        renderers = GetComponents<Renderer>();
    }
    public void SetMovingObject(Moving_Platform mo)
    {
        movingObject = mo;
    }

    // Update is called once per frame

    void Update()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            return;
        }
#endif
        if (movingObject)
        {
            movingObject.UpdatePositions();
        }
    }

    public void HideRenderers()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }
}
