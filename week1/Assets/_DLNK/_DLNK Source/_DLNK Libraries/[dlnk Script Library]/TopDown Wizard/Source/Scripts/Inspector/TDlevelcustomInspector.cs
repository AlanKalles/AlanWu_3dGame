using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TDLevel))]
public class TDlevelcustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        TDLevel level = (TDLevel)target;
        level.zongrup = GUILayout.TextField(level.zongrup);
        if (GUILayout.Button("Create New Zone Group")) 
        {
            level.OnDo();
        }

        DrawDefaultInspector();

    }
}
