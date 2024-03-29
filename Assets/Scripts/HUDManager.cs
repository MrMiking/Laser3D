using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text batteriesNumber;
    [SerializeField] private GameObject nexLevelPanel;

    public void OpenNextLevelPanel()
    {
        nexLevelPanel.SetActive(true);
    }
}
