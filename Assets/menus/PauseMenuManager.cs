using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuPanel;
    public GameObject settingsPanel; 
    public GameObject endScreenPanel; // NEW: Slot for your end screen

    [Header("Player References")]
    public MonoBehaviour playerCameraScript; 

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (playerCameraScript != null) playerCameraScript.enabled = false;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false); 
        if (endScreenPanel != null) endScreenPanel.SetActive(false); // NEW: Hide end screen if we resume

        Time.timeScale = 1f; 
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (playerCameraScript != null) playerCameraScript.enabled = true;
    }

    // NEW: This opens the End Screen instead of leaving the game
    public void OpenEndScreen()
    {
        pauseMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (endScreenPanel != null) endScreenPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        pauseMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    // This is your original function, we will just wire it to a different button now
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menus"); 
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game..."); 
        Application.Quit(); 
    }
}