using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private bool isVisible;

    private void Start()
    {
        panel.SetActive(false);
        isVisible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenCloseSettings();
        }
    }

    public void OpenCloseSettings()
    {
        panel.SetActive(!isVisible);
        isVisible = !isVisible;
    }
}
