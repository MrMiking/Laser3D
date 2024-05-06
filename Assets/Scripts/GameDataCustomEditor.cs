using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        GameData obj =  EditorUtility.InstanceIDToObject(instanceId) as GameData;
        if(obj == null)
        {
            GameDataEditorWindow.Open(obj);
            return true;
        }
        return false;
    }
}

[CustomEditor(typeof(GameData))]
public class GameDataCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open GameData Editor"))
        {
            GameDataEditorWindow.Open((GameData)target);
        }
    }
}
