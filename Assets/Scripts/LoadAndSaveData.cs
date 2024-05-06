using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadAndSaveData : MonoBehaviour
{
    public GameData gameData;

    string gameDataPath;

    private void Awake()
    {
        gameDataPath = Application.persistentDataPath + "/GamesData.json";

        if (GamesDataFileAlreadyExist()) LoadGamesData();
        else SaveGamesData();
    }

    public void SaveGamesData()
    {
        string infoData = JsonUtility.ToJson(gameData);
        File.WriteAllText(gameDataPath, infoData);
    }

    public void LoadGamesData()
    {
        string infoData = File.ReadAllText(gameDataPath);
        gameData = JsonUtility.FromJson<GameData>(infoData);
    }

    bool GamesDataFileAlreadyExist()
    {
        return File.Exists(gameDataPath);
    }
}
