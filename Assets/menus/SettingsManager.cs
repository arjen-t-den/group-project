using UnityEngine;
using UnityEngine.UI; // Change this to TMPro if you are using TextMeshPro Dropdowns
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    public Slider volumeSlider;
    public Slider brightnessSlider;
    public Slider bloomSlider;
    public Slider verticalSensitivitySlider;
    public Slider horizontalSensitivitySlider;
    public TMP_Dropdown colourblindDropdown; // If using TextMeshPro, change type to TMP_Dropdown

    [Header("Save Keys")]
    private string volumeKey = "Setting_Volume";
    private string brightnessKey = "Setting_Brightness";
    private string bloomKey = "Setting_Bloom";
    private string vSensKey = "Setting_VSens";
    private string hSensKey = "Setting_HSens";
    private string colourblindKey = "Setting_Colourblind";

    void Start()
    {
        // --- 1. LOAD ALL SAVED DATA ---
        
        // Load Sliders (Defaulting to 1f if the player has never played before)
        if (volumeSlider != null) volumeSlider.value = PlayerPrefs.GetFloat(volumeKey, 1f);
        if (brightnessSlider != null) brightnessSlider.value = PlayerPrefs.GetFloat(brightnessKey, 1f);
        if (bloomSlider != null) bloomSlider.value = PlayerPrefs.GetFloat(bloomKey, 1f);
        
        // For sensitivity, you might want a higher default depending on your game's math, e.g., 50f
        if (verticalSensitivitySlider != null) verticalSensitivitySlider.value = PlayerPrefs.GetFloat(vSensKey, 1f);
        if (horizontalSensitivitySlider != null) horizontalSensitivitySlider.value = PlayerPrefs.GetFloat(hSensKey, 1f);

        // Load Dropdown (Default is 0 / None)
        if (colourblindDropdown != null) colourblindDropdown.value = PlayerPrefs.GetInt(colourblindKey, 0);


        // --- 2. LISTEN FOR CHANGES ---
        
        if (volumeSlider != null) 
            volumeSlider.onValueChanged.AddListener(SaveVolume);
            
        if (brightnessSlider != null) 
            brightnessSlider.onValueChanged.AddListener(SaveBrightness);

        if (bloomSlider != null) 
            bloomSlider.onValueChanged.AddListener(SaveBloom);

        if (verticalSensitivitySlider != null) 
            verticalSensitivitySlider.onValueChanged.AddListener(SaveVerticalSensitivity);

        if (horizontalSensitivitySlider != null) 
            horizontalSensitivitySlider.onValueChanged.AddListener(SaveHorizontalSensitivity);
            
        if (colourblindDropdown != null) 
            colourblindDropdown.onValueChanged.AddListener(SaveColourblindMode);
    }

    // --- 3. SAVE FUNCTIONS ---

    private void SaveVolume(float value)
    {
        PlayerPrefs.SetFloat(volumeKey, value);
        PlayerPrefs.Save();
        Debug.Log("Volume saved: " + value);
    }

    private void SaveBrightness(float value)
    {
        PlayerPrefs.SetFloat(brightnessKey, value);
        PlayerPrefs.Save();
        Debug.Log("Brightness saved: " + value);
    }

    private void SaveBloom(float value)
    {
        PlayerPrefs.SetFloat(bloomKey, value);
        PlayerPrefs.Save();
        Debug.Log("Bloom saved: " + value);
    }

    private void SaveVerticalSensitivity(float value)
    {
        PlayerPrefs.SetFloat(vSensKey, value);
        PlayerPrefs.Save();
        Debug.Log("Vertical Sensitivity saved: " + value);
    }

    private void SaveHorizontalSensitivity(float value)
    {
        PlayerPrefs.SetFloat(hSensKey, value);
        PlayerPrefs.Save();
        Debug.Log("Horizontal Sensitivity saved: " + value);
    }

    private void SaveColourblindMode(int value)
    {
        PlayerPrefs.SetInt(colourblindKey, value);
        PlayerPrefs.Save();
        Debug.Log("Colourblind mode saved: " + value);
    }
}