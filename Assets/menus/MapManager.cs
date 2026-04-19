using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [Header("Map Toggles")]
    public Toggle officeToggle; 
    public Toggle factoryToggle;

    [Header("Locked Visuals")]
    public GameObject factoryLockedLine;

    [Header("Testing")]
    public bool forceUnlockFactory = false; // Tick this in Unity to test the unlocked state

    private string MapSaveKey = "SelectedMapIndex";
    
    // I added this variable so we never accidentally misspell the save key again!
    private string OfficeCompletedKey = "OfficeCompleted"; 

    void Start()
    {
        // Set up the locks before we try to load the saved map
        UpdateMapLocks();

        // Load the saved map, default to 0 (Office)
        int savedMap = PlayerPrefs.GetInt(MapSaveKey, 0);

        // Safety check: if factory is saved but currently locked, force back to office
        bool hasPlayedOffice = PlayerPrefs.GetInt(OfficeCompletedKey, 0) == 1 || forceUnlockFactory;
        if (savedMap == 1 && !hasPlayedOffice)
        {
            savedMap = 0;
        }

        // Apply the saved (or corrected) map choice
        if (savedMap == 0)
        {
            officeToggle.isOn = true;
        }
        else
        {
            factoryToggle.isOn = true;
        }

        // Listen for when the player clicks the toggles
        officeToggle.onValueChanged.AddListener(delegate { OnMapToggled(0, officeToggle.isOn); });
        factoryToggle.onValueChanged.AddListener(delegate { OnMapToggled(1, factoryToggle.isOn); });
    }

    private void UpdateMapLocks()
    {
        // We check if the Office has been completed
        bool hasPlayedOffice = PlayerPrefs.GetInt(OfficeCompletedKey, 0) == 1 || forceUnlockFactory;

        // Office map is always available
        officeToggle.interactable = true;

        // Factory map is only interactable if the office is complete
        factoryToggle.interactable = hasPlayedOffice;

        // Turn the red line on if locked, off if unlocked
        if (factoryLockedLine != null)
        {
            factoryLockedLine.SetActive(!hasPlayedOffice);
        }
    }

    private void OnMapToggled(int mapIndex, bool isOn)
    {
        // We only want to save when a toggle is turned ON, not when the old one turns OFF
        if (isOn)
        {
            PlayerPrefs.SetInt(MapSaveKey, mapIndex);
            PlayerPrefs.Save(); 
            Debug.Log("Map Selected: " + mapIndex);
        }
    }

    // You will call this function from your Office level when the player reaches the end
    public void FinishOfficeLevel()
    {
        PlayerPrefs.SetInt(OfficeCompletedKey, 1);
        PlayerPrefs.Save();
        Debug.Log("Office finished. Factory Map is now unlocked.");
    }

    public void StartGame()
    {
        // Check which map was saved. 0 is the default if nothing was saved.
        int selectedMap = PlayerPrefs.GetInt(MapSaveKey, 0);

        if (selectedMap == 0)
        {
            // Load the office. 
            SceneManager.LoadScene("Office Level");
        }
        else if (selectedMap == 1)
        {
            // Load the factory.
            SceneManager.LoadScene("Factory Level");
        }
    }
}