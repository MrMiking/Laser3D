using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject unlocked;

    private LevelData currentLevelData;

    private void Start()
    {
        for(int i = 0; i < gameData.levelsList.Count(); i++)
        {
            if (gameData.levelsList[i].name == transform.name)
            {
                currentLevelData = gameData.levelsList[i];

                if (currentLevelData.completed)
                {
                    locked.SetActive(false);
                    unlocked.SetActive(true);
                }
                else
                {
                    locked.SetActive(true);
                    unlocked.SetActive(false);
                }
            }
        }
    }
    public void LoadLevel()
    {
        if(currentLevelData.completed)
        SceneManager.LoadScene(transform.name);
    }
}
