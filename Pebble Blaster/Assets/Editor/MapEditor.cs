using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator map = target as MapGenerator;

        if (DrawDefaultInspector())//only if a value changes within the editor will it actually update the script attached
        {
            map.GenerateMap();
        }

        if(GUILayout.Button("Generate Map"))
        {
            map.GenerateMap();
        }
    }
}
