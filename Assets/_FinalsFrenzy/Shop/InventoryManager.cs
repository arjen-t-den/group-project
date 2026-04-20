using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private HashSet<string> ownedWeapons = new HashSet<string>();
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            UnityEngine.Debug.Log("Duplicate deleted");
        } else {
            Instance = this;          
        }
    }

    public void rememberWeapon(string weaponName)
    {
        ownedWeapons.Add(weaponName);
    }

    public void addWeapon(string weaponName)
    {
        ownedWeapons.Add(weaponName);
        PlayerPrefs.SetString(weaponName, "has");
    }
    public void removeWeapon(string weaponName)
    {
        ownedWeapons.Remove(weaponName);
        PlayerPrefs.DeleteKey(weaponName);
    }
    public bool isWeaponOwned(string weaponName)
    {
        return ownedWeapons.Contains(weaponName);
    }
}
