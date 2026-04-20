using UnityEngine;

public class OfficePlayTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float requiredPlayTime = 60f; 
    private float currentPlayTime = 0f;
    
    private bool hasUnlocked = false;
    private readonly string OfficeCompletedKey = "OfficeCompleted"; 

    void Start()
    {
        // Verify persistent unlock state on instantiation
        if (PlayerPrefs.GetInt(OfficeCompletedKey, 0) == 1)
        {
            hasUnlocked = true; 
        }
    }

    void Update()
    {
        // Terminate timer execution early if condition is already met
        if (hasUnlocked) return;

        // Accumulate active play time (pauses automatically if Time.timeScale == 0)
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
    }
}