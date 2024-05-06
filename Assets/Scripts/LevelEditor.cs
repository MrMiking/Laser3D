using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Unity.VisualScripting;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LevelEditor : EditorWindow
{
    public string sceneName;
    public GameData gameData;

    private List<string> levelNameList;

    private int levelIndex;

    [MenuItem("CustomTools/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditor>(false, "Level Editor", true);
    }

    private void OnGUI()
    {
        foreach (LevelData p in gameData.levelsList)
        {
            levelNameList.Add("World " + p.name);
        }
        levelIndex = EditorGUILayout.Popup(levelIndex, levelNameList.ToArray());
        gameData.levelsList[levelIndex].backgroundColor = EditorGUILayout.ColorField("Background Color", gameData.levelsList[levelIndex].backgroundColor);
    }
}
