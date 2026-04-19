using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuPanel;
    public GameObject settingsPanel; 

    private bool isPaused = false;

    void Update()
    {
        // Toggle the pause menu when the Escape key is pressed
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
        Time.timeScale = 0f; // Freezes the game
        isPaused = true;

        // This unlocks the mouse and makes it visible so you can click buttons
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

   public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false); 
        Time.timeScale = 1f; // Unfreezes the game
        isPaused = false;

        // This hides the mouse and locks it back to the game window
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMainMenu()
    {
        // We must unfreeze time before leaving the scene!
        Time.timeScale = 1f; 
        
        // Replace "MainMenu" with the exact name of your main menu scene
        SceneManager.LoadScene("Menus"); 
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

    public void QuitGame()
    {
        Debug.Log("Quitting Game..."); 
        Application.Quit(); 
    }
}