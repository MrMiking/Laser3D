using UnityEngine;

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
}
