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
    // Weapon 1 is always unlocked
    public bool weapon2Unlocked = false;
    public bool weapon3Unlocked = false;

    private string WeaponSaveKey = "SelectedWeaponIndex";

    void Start()
    {
        // Check unlock states and set initial UI
        InitializeWeaponState(weapon1Toggle, weapon1LockedLine, true);
        InitializeWeaponState(weapon2Toggle, weapon2LockedLine, weapon2Unlocked);
        InitializeWeaponState(weapon3Toggle, weapon3LockedLine, weapon3Unlocked);

        int savedWeapon = PlayerPrefs.GetInt(WeaponSaveKey, 0);

        // Safety check: reset to weapon 0 if the saved one is currently locked
        if (savedWeapon == 1 && !weapon2Unlocked) savedWeapon = 0;
        if (savedWeapon == 2 && !weapon3Unlocked) savedWeapon = 0;

        // Set initial toggle state
        if (savedWeapon == 0) weapon1Toggle.isOn = true;
        else if (savedWeapon == 1) weapon2Toggle.isOn = true;
        else if (savedWeapon == 2) weapon3Toggle.isOn = true;

        // Add listeners
        weapon1Toggle.onValueChanged.AddListener(delegate { OnWeaponToggled(0, weapon1Toggle.isOn); });
        weapon2Toggle.onValueChanged.AddListener(delegate { OnWeaponToggled(1, weapon2Toggle.isOn); });
        weapon3Toggle.onValueChanged.AddListener(delegate { OnWeaponToggled(2, weapon3Toggle.isOn); });
    }

    private void InitializeWeaponState(Toggle toggle, GameObject lockedLine, bool isUnlocked)
    {
        // Set toggle interactability
        toggle.interactable = isUnlocked;

        // Set locked line visibility
        if (lockedLine != null)
        {
            lockedLine.SetActive(!isUnlocked);
        }
    }

    private void OnWeaponToggled(int index, bool state)
    {
        // Only save when the toggle is actually selected and is unlocked
        if (state)
        {
            PlayerPrefs.SetInt(WeaponSaveKey, index);
            PlayerPrefs.Save();
            Debug.Log("Weapon saved: " + index);
        }
    }
}