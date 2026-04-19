using UnityEngine;
using UnityEngine.UI;
using TMPro; // We need this to talk to your text numbers!

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
    private string volumeKey = "Setting_Volume";
    private string brightnessKey = "Setting_Brightness";
    private string bloomKey = "Setting_Bloom";
    private string vSensKey = "Setting_VSens";
    private string hSensKey = "Setting_HSens";
    private string colourblindKey = "Setting_Colourblind";

    void Start()
    {
        // --- 1. LOAD DATA & SET SLIDERS ---
        // Notice we are now defaulting to 50f instead of 1f
        if (volumeSlider != null) volumeSlider.value = PlayerPrefs.GetFloat(volumeKey, 50f);
        if (brightnessSlider != null) brightnessSlider.value = PlayerPrefs.GetFloat(brightnessKey, 50f);
        if (bloomSlider != null) bloomSlider.value = PlayerPrefs.GetFloat(bloomKey, 50f);
        if (verticalSensitivitySlider != null) verticalSensitivitySlider.value = PlayerPrefs.GetFloat(vSensKey, 50f);
        if (horizontalSensitivitySlider != null) horizontalSensitivitySlider.value = PlayerPrefs.GetFloat(hSensKey, 50f);

        if (colourblindDropdown != null) colourblindDropdown.value = PlayerPrefs.GetInt(colourblindKey, 0);

        // --- 2. UPDATE TEXT NUMBERS ON START ---
        UpdateText(volumeText, volumeSlider.value);
        UpdateText(brightnessText, brightnessSlider.value);
        UpdateText(bloomText, bloomSlider.value);
        UpdateText(vSensText, verticalSensitivitySlider.value);
        UpdateText(hSensText, horizontalSensitivitySlider.value);

        // --- 3. LISTEN FOR CHANGES ---
        if (volumeSlider != null) volumeSlider.onValueChanged.AddListener(SaveVolume);
        if (brightnessSlider != null) brightnessSlider.onValueChanged.AddListener(SaveBrightness);
        if (bloomSlider != null) bloomSlider.onValueChanged.AddListener(SaveBloom);
        if (verticalSensitivitySlider != null) verticalSensitivitySlider.onValueChanged.AddListener(SaveVerticalSensitivity);
        if (horizontalSensitivitySlider != null) horizontalSensitivitySlider.onValueChanged.AddListener(SaveHorizontalSensitivity);
        if (colourblindDropdown != null) colourblindDropdown.onValueChanged.AddListener(SaveColourblindMode);
    }

    // A handy function to update the text labels easily
    private void UpdateText(TextMeshProUGUI textElement, float value)
    {
        if (textElement != null)
        {
            // "0" formats the number as a whole number (no crazy decimals like 50.342)
            textElement.text = value.ToString("0"); 
        }
    }

    // --- 4. SAVE FUNCTIONS ---

    private void SaveVolume(float value)
    {
        PlayerPrefs.SetFloat(volumeKey, value);
        PlayerPrefs.Save();
        UpdateText(volumeText, value); // Updates the number instantly when moving
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