using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionData : MonoBehaviour
{
    public static WeaponSelectionData Instance;
    public List<string> selectedWeaponNames = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetSelection()
    {
        selectedWeaponNames.Clear();
    }

    public void AddWeapon(string weaponName)
    {
        if (!selectedWeaponNames.Contains(weaponName))
        {
            selectedWeaponNames.Add(weaponName);
        }
    }
}