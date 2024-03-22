using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    [SerializeField] private int activatedBattery;
    [SerializeField] private HUDManager hudManager;

    private void Awake()
    {
        hudManager = GameObject.Find("HUDManager").GetComponent<HUDManager>();
    }

    private void Update()
    {
        hudManager.UpdateBatteryText(activatedBattery);
    }

    public void AddBattery()
    {
        activatedBattery += 1;
    }
    public void RemoveBattery()
    {
        if (activatedBattery > 0) activatedBattery -= 1;
    }
}
