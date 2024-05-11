using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    [SerializeField] bool isActivated;

    private void Awake()
    {
        levelManager = GameObject.Find("SceneManager").GetComponent<LevelManager>();
    }

    public void ActiveBattery()
    {
        if (!isActivated)
        {
            isActivated = true;
            levelManager.AddBattery();
        }
    }

    public void DesactiveBattery()
    {
        if (isActivated)
        {
            isActivated = false;
            levelManager.RemoveBattery();
        }
    }
}
