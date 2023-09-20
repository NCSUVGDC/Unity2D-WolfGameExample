using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Moving_Platform))]
public class MovingPlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Moving_Platform mp = (Moving_Platform)target;
        if (GUILayout.Button("Add Movement Goal"))
        {
            if (mp.IsSceneBound())
            {
                mp.AddNewGoal();
            }
        }
        // if (GUILayout.Button("Display Paths"))
        // {
        //     if (zpg.IsSceneBound())
        //     {
        //         zpg.DebugLines();
        //     }
        // }
    }
}
