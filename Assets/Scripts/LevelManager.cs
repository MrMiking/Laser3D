using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int batteryToActivate;

    [SerializeField] private int activatedBattery;
    [SerializeField] private HUDManager hudManager;

    private void Awake()
    {
        hudManager = GameObject.Find("HUDManager").GetComponent<HUDManager>();
    }

    private void Update()
    {
        if(activatedBattery >= batteryToActivate)
        {
            StartCoroutine(NextLevelAnimation());
        }
    }

    public void AddBattery()
    {
        activatedBattery += 1;
    }
    public void RemoveBattery()
    {
        if (activatedBattery > 0) activatedBattery -= 1;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator NextLevelAnimation()
    {
        yield return new WaitForSeconds(.5f);
        hudManager.OpenNextLevelPanel();
    }
}
