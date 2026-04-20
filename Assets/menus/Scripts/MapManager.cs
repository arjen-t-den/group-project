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
    public bool forceUnlockFactory = false; 

    private readonly string MapSaveKey = "SelectedMapIndex";
    private readonly string OfficeCompletedKey = "OfficeCompleted"; 

    void Start()
    {
        UpdateMapLocks();

        int savedMap = PlayerPrefs.GetInt(MapSaveKey, 0);

        // Fallback mechanism to prevent loading locked content
        bool hasPlayedOffice = PlayerPrefs.GetInt(OfficeCompletedKey, 0) == 1 || forceUnlockFactory;
        if (savedMap == 1 && !hasPlayedOffice)
        {
            savedMap = 0;
        }

        // Apply state to toggles
        if (savedMap == 0) officeToggle.isOn = true;
        else factoryToggle.isOn = true;

        // Register event listeners
        officeToggle.onValueChanged.AddListener(delegate { OnMapToggled(0, officeToggle.isOn); });
        factoryToggle.onValueChanged.AddListener(delegate { OnMapToggled(1, factoryToggle.isOn); });
    }

    private void UpdateMapLocks()
    {
        bool hasPlayedOffice = PlayerPrefs.GetInt(OfficeCompletedKey, 0) == 1 || forceUnlockFactory;

        officeToggle.interactable = true;
        factoryToggle.interactable = hasPlayedOffice;

        if (factoryLockedLine != null)
        {
            factoryLockedLine.SetActive(!hasPlayedOffice);
        }
    }

    private void OnMapToggled(int mapIndex, bool isOn)
    {
        // Persist data only for the toggle entering the active state
        if (isOn)
        {
            PlayerPrefs.SetInt(MapSaveKey, mapIndex);
            PlayerPrefs.Save(); 
        }
    }

    public void FinishOfficeLevel()
    {
        PlayerPrefs.SetInt(OfficeCompletedKey, 1);
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        int selectedMap = PlayerPrefs.GetInt(MapSaveKey, 0);

        // Route to the appropriate scene index
        if (selectedMap == 0)
        {
            SceneManager.LoadScene("Office Level");
        }
        else if (selectedMap == 1)
        {
            SceneManager.LoadScene("Factory Level");
        }
    }
}