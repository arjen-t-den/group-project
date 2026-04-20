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
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        mapSelectPanel.SetActive(false);
        weaponsPanel.SetActive(false);
    }

    public void ShowSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        mapSelectPanel.SetActive(false);
        weaponsPanel.SetActive(false);
    }

    public void ShowMapSelect()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        mapSelectPanel.SetActive(true);
        weaponsPanel.SetActive(false);
    }

    public void ShowWeapons()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        mapSelectPanel.SetActive(false);
        weaponsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}