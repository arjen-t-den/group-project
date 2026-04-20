using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuPanel;
    public GameObject settingsPanel; 
    public GameObject endScreenPanel;

    [Header("Player References")]
    public MonoBehaviour playerCameraScript; 

    private bool isPaused = false;

    void Update()
    {
        // Toggle pause state on Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;

        // Unlock cursor for UI interaction
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Disable player camera input while paused
        if (playerCameraScript != null) playerCameraScript.enabled = false;
    }

    public void ResumeGame()
    {
        // Ensure all pause-related UI is hidden
        pauseMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false); 
        if (endScreenPanel != null) endScreenPanel.SetActive(false);

        Time.timeScale = 1f; 
        isPaused = false;

        // Relock cursor for gameplay
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Re-enable player camera input
        if (playerCameraScript != null) playerCameraScript.enabled = true;
    }

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

    public void LoadMainMenu()
    {
        // Restore time scale before scene transition to prevent locked states
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menus"); 
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}