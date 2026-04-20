using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class SettingsManager : MonoBehaviour
{
    [Header("UI Sliders")]
    public Slider volumeSlider;
    public Slider brightnessSlider;
    public Slider bloomSlider;
    public Slider verticalSensitivitySlider;
    public Slider horizontalSensitivitySlider;

    [Header("UI Numbers (Text)")]
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI brightnessText;
    public TextMeshProUGUI bloomText;
    public TextMeshProUGUI vSensText;
    public TextMeshProUGUI hSensText;

    [Header("Dropdown")]
    public TMP_Dropdown colourblindDropdown; 

    [Header("Save Keys")]
    private readonly string volumeKey = "Setting_Volume";
    private readonly string brightnessKey = "Setting_Brightness";
    private readonly string bloomKey = "Setting_Bloom";
    private readonly string vSensKey = "Setting_VSens";
    private readonly string hSensKey = "Setting_HSens";
    private readonly string colourblindKey = "Setting_Colourblind";

    void Start()
    {
        // Initialize UI elements with saved preferences (defaulting to 50f / 0)
        if (volumeSlider != null) volumeSlider.value = PlayerPrefs.GetFloat(volumeKey, 50f);
        if (brightnessSlider != null) brightnessSlider.value = PlayerPrefs.GetFloat(brightnessKey, 50f);
        if (bloomSlider != null) bloomSlider.value = PlayerPrefs.GetFloat(bloomKey, 50f);
        if (verticalSensitivitySlider != null) verticalSensitivitySlider.value = PlayerPrefs.GetFloat(vSensKey, 50f);
        if (horizontalSensitivitySlider != null) horizontalSensitivitySlider.value = PlayerPrefs.GetFloat(hSensKey, 50f);

        if (colourblindDropdown != null) colourblindDropdown.value = PlayerPrefs.GetInt(colourblindKey, 0);

        // Synchronize text readouts with initial slider values
        UpdateText(volumeText, volumeSlider.value);
        UpdateText(brightnessText, brightnessSlider.value);
        UpdateText(bloomText, bloomSlider.value);
        UpdateText(vSensText, verticalSensitivitySlider.value);
        UpdateText(hSensText, horizontalSensitivitySlider.value);

        // Register event listeners for UI state changes
        if (volumeSlider != null) volumeSlider.onValueChanged.AddListener(SaveVolume);
        if (brightnessSlider != null) brightnessSlider.onValueChanged.AddListener(SaveBrightness);
        if (bloomSlider != null) bloomSlider.onValueChanged.AddListener(SaveBloom);
        if (verticalSensitivitySlider != null) verticalSensitivitySlider.onValueChanged.AddListener(SaveVerticalSensitivity);
        if (horizontalSensitivitySlider != null) horizontalSensitivitySlider.onValueChanged.AddListener(SaveHorizontalSensitivity);
        if (colourblindDropdown != null) colourblindDropdown.onValueChanged.AddListener(SaveColourblindMode);
    }

    private void UpdateText(TextMeshProUGUI textElement, float value)
    {
        if (textElement != null)
        {
            // Format float to zero decimal places for UI readability
            textElement.text = value.ToString("0"); 
        }
    }

    private void SaveVolume(float value)
    {
        PlayerPrefs.SetFloat(volumeKey, value);
        PlayerPrefs.Save();
        UpdateText(volumeText, value); 
    }

    private void SaveBrightness(float value)
    {
        PlayerPrefs.SetFloat(brightnessKey, value);
        PlayerPrefs.Save();
        UpdateText(brightnessText, value);
    }

    private void SaveBloom(float value)
    {
        PlayerPrefs.SetFloat(bloomKey, value);
        PlayerPrefs.Save();
        UpdateText(bloomText, value);
    }

    private void SaveVerticalSensitivity(float value)
    {
        PlayerPrefs.SetFloat(vSensKey, value);
        PlayerPrefs.Save();
        UpdateText(vSensText, value);
    }

    private void SaveHorizontalSensitivity(float value)
    {
        PlayerPrefs.SetFloat(hSensKey, value);
        PlayerPrefs.Save();
        UpdateText(hSensText, value);
    }

    private void SaveColourblindMode(int value)
    {
        PlayerPrefs.SetInt(colourblindKey, value);
        PlayerPrefs.Save();
    }
}