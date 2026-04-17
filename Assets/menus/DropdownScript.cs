using UnityEngine;
using TMPro;

public class DropdownScript : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private string dropdownId = "ColourblindSetting";
    
    // Fallback index
    [SerializeField] private int defaultValue = 0; 

    private string DropdownValueKey => $"DropdownValue_{dropdownId}";

    void Start()
    {
        _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        LoadDropdownValue();
    }

    private void OnDropdownValueChanged(int value)
    {
        SaveDropdownValue(value);
        
        // TODO: hook up the actual colour filter logic here
        Debug.Log("Colour mode index: " + value);
    }

    private void LoadDropdownValue()
    {
        int savedValue = PlayerPrefs.GetInt(DropdownValueKey, defaultValue);
        _dropdown.value = savedValue;
        
        // Save initial state if missing
        if (!PlayerPrefs.HasKey(DropdownValueKey))
        {
            SaveDropdownValue(savedValue);
        }
    }

    private void SaveDropdownValue(int value)
    {
        PlayerPrefs.SetInt(DropdownValueKey, value);
        PlayerPrefs.Save();
    }
}