using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [Header("UI References")]
    public Toggle weapon1Toggle;
    public Toggle weapon2Toggle;
    public Toggle weapon3Toggle;

    private string WeaponSaveKey = "SelectedWeaponIndex";

    void Start()
    {
        // Get saved weapon index, default to first weapon (0)
        int savedWeapon = PlayerPrefs.GetInt(WeaponSaveKey, 0);

        // Set initial toggle state
        if (savedWeapon == 0) weapon1Toggle.isOn = true;
        else if (savedWeapon == 1) weapon2Toggle.isOn = true;
        else if (savedWeapon == 2) weapon3Toggle.isOn = true;

        // Add listeners for value changes
        weapon1Toggle.onValueChanged.AddListener(delegate { OnWeaponToggled(0, weapon1Toggle.isOn); });
        weapon2Toggle.onValueChanged.AddListener(delegate { OnWeaponToggled(1, weapon2Toggle.isOn); });
        weapon3Toggle.onValueChanged.AddListener(delegate { OnWeaponToggled(2, weapon3Toggle.isOn); });
    }

    private void OnWeaponToggled(int index, bool state)
    {
        // Only save when the toggle is actually selected
        if (state)
        {
            PlayerPrefs.SetInt(WeaponSaveKey, index);
            PlayerPrefs.Save();
            Debug.Log("Weapon saved: " + index);
        }
    }
}