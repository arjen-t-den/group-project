using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject mapSelectPanel;
    public GameObject weaponsPanel;

    void Start()
    {
        // Set initial state on load
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        mapSelectPanel.SetActive(false);
        finishScreenPanel.SetActive(false);
        weaponsPanel.SetActive(false);
    }

    public void ShowSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        mapSelectPanel.SetActive(false);
        finishScreenPanel.SetActive(false);
        weaponsPanel.SetActive(false);
    }

    public void ShowMapSelect()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        mapSelectPanel.SetActive(true);
        finishScreenPanel.SetActive(false);
        weaponsPanel.SetActive(false);
    }

    public void ShowWeapons()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        mapSelectPanel.SetActive(false);
        finishScreenPanel.SetActive(false);
        weaponsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit application");
        Application.Quit();
    }
}