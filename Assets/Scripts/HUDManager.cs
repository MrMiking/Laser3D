using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text batteriesNumber;

    public void UpdateBatteryText(int quantity)
    {
        batteriesNumber.text = $"Battery : {quantity}";
    }
}
