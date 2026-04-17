using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [Header("Map Toggles")]
    public Toggle tutorialToggle; // Changed from streetToggle
    public Toggle officeToggle;

    [Header("Locked Visuals")]
    public GameObject officeLockedLine;

    [Header("Testing")]
    public bool forceUnlockOffice = false; // Tick this in Unity to test the unlocked state

    private string MapSaveKey = "SelectedMapIndex";

    void Start()
    {
        // Set up the locks before we try to load the saved map
        UpdateMapLocks();

        // Load the saved map, default to 0 (Tutorial)
        int savedMap = PlayerPrefs.GetInt(MapSaveKey, 0);

        // Safety check: if office is saved but currently locked, force back to tutorial
        bool hasPlayedTutorial = PlayerPrefs.GetInt("TutorialCompleted", 0) == 1 || forceUnlockOffice;
        if (savedMap == 1 && !hasPlayedTutorial)
        {
            savedMap = 0;
        }

        // Apply the saved (or corrected) map choice
        if (savedMap == 0)
        {
            tutorialToggle.isOn = true;
        }
        else
        {
            officeToggle.isOn = true;
        }

        // Listen for when the player clicks the toggles
        tutorialToggle.onValueChanged.AddListener(delegate { OnMapToggled(0, tutorialToggle.isOn); });
        officeToggle.onValueChanged.AddListener(delegate { OnMapToggled(1, officeToggle.isOn); });
    }

    private void UpdateMapLocks()
    {
        // We check if "TutorialCompleted" is saved as 1 (true)
        bool hasPlayedTutorial = PlayerPrefs.GetInt("TutorialCompleted", 0) == 1 || forceUnlockOffice;

        // Tutorial map is always available
        tutorialToggle.interactable = true;

        // Office map is only interactable if the tutorial is complete
        officeToggle.interactable = hasPlayedTutorial;

        // Turn the red line on if locked, off if unlocked
        if (officeLockedLine != null)
        {
            officeLockedLine.SetActive(!hasPlayedTutorial);
        }
    }

    private void OnMapToggled(int mapIndex, bool isOn)
    {
        // We only want to save when a toggle is turned ON, not when the old one turns OFF
        if (isOn)
        {
            PlayerPrefs.SetInt(MapSaveKey, mapIndex);
            PlayerPrefs.Save(); // Added a save call to ensure the choice is stored immediately
            Debug.Log("Map Selected: " + mapIndex);
        }
    }

    // You will call this function from your Tutorial level when the player reaches the end
    public void FinishTutorialLevel()
    {
        PlayerPrefs.SetInt("TutorialCompleted", 1);
        PlayerPrefs.Save();
        Debug.Log("Tutorial finished. Office Map is now unlocked.");
    }
}