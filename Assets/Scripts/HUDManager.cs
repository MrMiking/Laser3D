using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentLevelText;

    private LevelManager levelManager;
    private LevelData currentLevelData;

    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();
    }

    private void Start()
    {
        currentLevelData = levelManager.gameData.levelsList[levelManager.GetCurrentLevelIndex()];

        currentLevelText.text = currentLevelData.name[5].ToString() + " - " + currentLevelData.name[8].ToString() + currentLevelData.name[9].ToString();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Lvl_Menu");
    }
}
