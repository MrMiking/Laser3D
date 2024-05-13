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
    public GameData gameData;
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

    public int GetCurrentLevelIndex()
    {
        for (int i = 0; i < gameData.levelsList.Count(); i++)
        {
            if (gameData.levelsList[i].sceneName == SceneManager.GetActiveScene().name)
            {
                return i;
            }
        }
        Debug.Log("Scene Index Not Found");
        return 0;
    }

    public void AddBattery()
    {
        activatedBattery += 1;
        if (activatedBattery >= batteryToActivate)
        {
            StartCoroutine(NextLevel());
        }
    }
    public void RemoveBattery()
    {
        if (activatedBattery > 0) activatedBattery -= 1;
    }

    IEnumerator NextLevel()
    {
        Vector3 startPosition = levelCamera.transform.position;
        Vector3 endPosition = new Vector3(levelCamera.transform.position.x + 10, levelCamera.transform.position.y, levelCamera.transform.position.z - 10);
        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            levelCamera.transform.position = Vector3.Lerp(startPosition, endPosition, t / 0.5f);
            yield return null;
        }
        levelCamera.transform.position = endPosition;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        yield return new WaitForSeconds(0.5f);
    }
}
