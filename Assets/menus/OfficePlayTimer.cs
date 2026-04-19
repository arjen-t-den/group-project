using UnityEngine;

public class OfficePlayTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float requiredPlayTime = 60f; // 60 seconds = 1 minute
    private float currentPlayTime = 0f;
    
    private bool hasUnlocked = false;
    
    // This MUST match the exact spelling in your MapManager!
    private string OfficeCompletedKey = "OfficeCompleted"; 

    void Start()
    {
        // Check if the player already unlocked the map in a previous session
        if (PlayerPrefs.GetInt(OfficeCompletedKey, 0) == 1)
        {
            hasUnlocked = true; 
        }
    }

    void Update()
    {
        // If the map is already unlocked, we stop counting to save processing power
        if (hasUnlocked) return;

        // Time.deltaTime automatically stops counting when your pause menu sets Time.timeScale to 0!
        currentPlayTime += Time.deltaTime;

        if (currentPlayTime >= requiredPlayTime)
        {
            UnlockFactoryMap();
        }
    }

    private void UnlockFactoryMap()
    {
        hasUnlocked = true;
        PlayerPrefs.SetInt(OfficeCompletedKey, 1);
        PlayerPrefs.Save();
        
        Debug.Log("1 minute reached! The Factory Level is now unlocked.");
    }
}