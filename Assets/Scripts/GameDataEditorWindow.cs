using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class GameDataEditorWindow : EditorWindow
{
    public static void Open(GameData dataObject)
    {
        GameDataEditorWindow window = GetWindow<GameDataEditorWindow>("GameData Editor");
        window.Serialize
    }

    private void OnGUI()
    {
        
    }
}
