using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [Header("UI References")]
    public Toggle weapon1Toggle;
    public Toggle weapon2Toggle;
    public Toggle weapon3Toggle;

    [Header("Locked Line References")]
    public GameObject weapon1LockedLine; 
    public GameObject weapon2LockedLine; 
    public GameObject weapon3LockedLine; 

    [Header("Unlock States (For Testing)")]
    public bool weapon2Unlocked = false;
    public bool weapon3Unlocked = false;

    private readonly string WeaponSaveKey = "SelectedWeaponIndex";

    void Start()
    {
        // Initialize UI interactability and visual feedback
        InitializeWeaponState(weapon1Toggle, weapon1LockedLine, true);
        InitializeWeaponState(weapon2Toggle, weapon2LockedLine, weapon2Unlocked);
        InitializeWeaponState(weapon3Toggle, weapon3LockedLine, weapon3Unlocked);

        int savedWeapon = PlayerPrefs.GetInt(WeaponSaveKey, 0);

        // Fallback to default index if saved selection is currently locked
        if (savedWeapon == 1 && !weapon2Unlocked) savedWeapon = 0;
        if (savedWeapon == 2 && !weapon3Unlocked) savedWeapon = 0;

        // Apply state to toggles
        if (savedWeapon == 0) weapon1Toggle.isOn = true;
        else if (savedWeapon == 1) weapon2Toggle.isOn = true;
        else if (savedWeapon == 2) weapon3Toggle.isOn = true;

        // Register event listeners
        weapon1Toggle.onValueChanged.AddListener(delegate { OnWeaponToggled(0, weapon1Toggle.isOn); });
        weapon2Toggle.onValueChanged.AddListener(delegate { OnWeaponToggled(1, weapon2Toggle.isOn); });
        weapon3Toggle.onValueChanged.AddListener(delegate { OnWeaponToggled(2, weapon3Toggle.isOn); });
    }

    private void InitializeWeaponState(Toggle toggle, GameObject lockedLine, bool isUnlocked)
    {
        toggle.interactable = isUnlocked;

        if (lockedLine != null)
        {
            lockedLine.SetActive(!isUnlocked);
        }
    }

    private void OnWeaponToggled(int index, bool state)
    {
        // Persist data only for the toggle entering the active state
        if (state)
        {
            PlayerPrefs.SetInt(WeaponSaveKey, index);
            PlayerPrefs.Save();
        }
    }
}