using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int batteryToActivate;

    [SerializeField] private int activatedBattery;

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
