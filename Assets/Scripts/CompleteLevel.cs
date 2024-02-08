using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public GameObject endPanel;

    private void Start()
    {
        endPanel.SetActive(false);
    }

    public void Complete()
    {
        endPanel.SetActive(true);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
