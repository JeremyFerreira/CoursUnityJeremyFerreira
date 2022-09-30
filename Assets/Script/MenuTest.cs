using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class MenuTest : MonoBehaviour
{
    // Add a menu item named "Do Something" to MyMenu in the menu bar.
    [MenuItem("Tools/Log Console ")]
    static void DebugSelectedObject()
    {
        for(int i = 0; i < Selection.gameObjects.Length; i++)
        {
            Debug.Log(Selection.gameObjects[i].name);
        }
    }

    [MenuItem("Tools/Log Console", true)]
    static bool ValidateLogSelectedTransformName()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }

    
}
