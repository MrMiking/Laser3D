using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    [SerializeField] private GameData gameData;

    public string sceneName;

    private List<string> levelNameList;

    private int levelIndex;

    [MenuItem("CustomTools/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditor>(false, "Level Editor", true);
    }

    private void OnGUI()
    {
        gameData = (GameData)EditorGUILayout.ObjectField(gameData, typeof(GameData), true);
        
        if(gameData != null)
        {
            Debug.Log(gameData.levelsList[0].name);
            foreach (LevelData p in gameData.levelsList)
            {
                levelNameList.Add("World " + p.name[4] + p.name[5] + " : Level " + p.name[7] + p.name[8] + p.name[9]);
            }
            levelIndex = EditorGUILayout.Popup(levelIndex, levelNameList.ToArray());
            gameData.levelsList[levelIndex].backgroundColor = EditorGUILayout.ColorField("Background Color", gameData.levelsList[levelIndex].backgroundColor);
            gameData.levelsList[levelIndex].cameraZoom = EditorGUILayout.IntField("Camera Zoom", gameData.levelsList[levelIndex].cameraZoom);

            if (GUILayout.Button("Open Scene"))
            {
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
                EditorSceneManager.OpenScene("Assets/Scenes/" + gameData.levelsList[levelIndex].name + ".unity");
            }
        }
    }
}
