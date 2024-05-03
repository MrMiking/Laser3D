using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int batteryToActivate;
    [SerializeField] private Camera levelCamera;
    [SerializeField] private GameData gameData;

    LevelData currentLevelData;

    private int activatedBattery;

    private void Start()
    {
        currentLevelData = GetCurrentLevel();

        levelCamera.backgroundColor = currentLevelData.backgroundColor;
        levelCamera.orthographicSize = currentLevelData.cameraZoom;
    }

    public LevelData GetCurrentLevel()
    {
        for(int i = 0; i < gameData.levelsList.Count(); i++)
        {
            if (gameData.levelsList[i].levelName == SceneManager.GetActiveScene().name)
            {
                return gameData.levelsList[i];
            }
        }
        Debug.Log("Scene Not Found");
        return gameData.levelsList[0];
    }

    public void AddBattery()
    {
        activatedBattery += 1;
        if (activatedBattery >= batteryToActivate)
        {
            NextLevel();
        }
    }
    public void RemoveBattery()
    {
        if (activatedBattery > 0) activatedBattery -= 1;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
