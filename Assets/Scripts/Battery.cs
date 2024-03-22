using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] private BatteryManager batteryManager;

    [SerializeField] bool isActivated;

    private void Awake()
    {
        batteryManager = GameObject.Find("BatteryManager").GetComponent<BatteryManager>();
    }

    public void ActiveBattery()
    {
        if (!isActivated)
        {
            isActivated = true;
            batteryManager.AddBattery();
        }
    }

    public void DesactiveBattery()
    {
        if (isActivated)
        {
            isActivated = false;
            batteryManager.RemoveBattery();
        }
    }
}
