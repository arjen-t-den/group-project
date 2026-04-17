using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [Header("Map Toggles")]
    public Toggle streetToggle;
    public Toggle officeToggle;

    private string MapSaveKey = "SelectedMapIndex";

    void Start()
    {
        // Load the saved map, default to 0 (Street)
        int savedMap = PlayerPrefs.GetInt(MapSaveKey, 0);

        if (savedMap == 0)
        {
            streetToggle.isOn = true;
        }
        else
        {
            officeToggle.isOn = true;
        }

        // Listen for when the player clicks the toggles
        streetToggle.onValueChanged.AddListener(delegate { OnMapToggled(0, streetToggle.isOn); });
        officeToggle.onValueChanged.AddListener(delegate { OnMapToggled(1, officeToggle.isOn); });
    }

    private void OnMapToggled(int mapIndex, bool isOn)
    {
        // We only want to save when a toggle is turned ON, not when the old one turns OFF
        if (isOn)
        {
            PlayerPrefs.SetInt(MapSaveKey, mapIndex);
            Debug.Log("Map Selected: " + mapIndex);
        }
    }
}