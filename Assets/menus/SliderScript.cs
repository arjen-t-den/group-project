using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    
    [SerializeField] private string sliderId = "DefaultSlider";
    
    // This allows you to customise the starting number in the Inspector for each slider
    [SerializeField] private float defaultValue = 50f;

    private string SliderValueKey => $"SliderValue_{sliderId}";

    void Start()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
        LoadSliderValue();
    }

    private void OnSliderValueChanged(float value)
    {
        _sliderText.text = value.ToString("0");
        SaveSliderValue(value);
        
        if (sliderId == "Volume")
        {
            AudioListener.volume = value / 100f;
        }
    }

    private void LoadSliderValue()
    {
        // PlayerPrefs.GetFloat can take a second argument. 
        // If it cannot find a saved value, it uses your defaultValue instead.
        float savedValue = PlayerPrefs.GetFloat(SliderValueKey, defaultValue);
        
        _slider.value = savedValue;
        _sliderText.text = savedValue.ToString("0");
        
        if (sliderId == "Volume")
        {
            AudioListener.volume = savedValue / 100f;
        }
        
        // This ensures the default value is saved to the computer immediately 
        // on the very first playthrough.
        if (!PlayerPrefs.HasKey(SliderValueKey))
        {
            SaveSliderValue(savedValue);
        }
    }

    private void SaveSliderValue(float value)
    {
        PlayerPrefs.SetFloat(SliderValueKey, value);
    }
}