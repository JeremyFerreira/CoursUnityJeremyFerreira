using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
// Custom Editor using SerializedProperties.
// Automatic handling of multi-object editing, undo, and Prefab overrides.
[CustomEditor(typeof(StringExposed))]
public class ScriptEditor : Editor
{
    SerializedProperty stringExposed;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        stringExposed = serializedObject.FindProperty("myString");
    }
    public override void OnInspectorGUI()
    {
        if (stringExposed.stringValue != "")
        {

            EditorGUILayout.LabelField(stringExposed.stringValue);
        }
        base.OnInspectorGUI();
    }

}
