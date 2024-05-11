using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class LevelManager : MonoBehaviour
{
    [SerializeField] private int batteryToActivate;

    private int activatedBattery;

    [SerializeField] private Camera levelCamera;
    [SerializeField] private GameData gameData;
    private void Update()
    {
        levelCamera.backgroundColor = GetCurrentLevel().backgroundColor;
        levelCamera.orthographicSize = GetCurrentLevel().cameraZoom;
    }

    public LevelData GetCurrentLevel()
    {
        for (int i = 0; i < gameData.levelsList.Count(); i++)
        {
            if (gameData.levelsList[i].sceneName == SceneManager.GetActiveScene().name)
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
