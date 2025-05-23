using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelectionUI : MonoBehaviour
{
    public void SelectPrimary(string weaponName)
    {
        GameManager.Instance.primaryWeaponName = weaponName;
    }

    public void SelectSecondary(string weaponName)
    {
        GameManager.Instance.secondaryWeaponName = weaponName;
    }

    public void StartGame(string gameplayScene)
    {
        // Check that both weapon names are set and not empty
        if (!string.IsNullOrEmpty(GameManager.Instance.primaryWeaponName) &&
            !string.IsNullOrEmpty(GameManager.Instance.secondaryWeaponName))
        {
            SceneManager.LoadScene(gameplayScene);
        }
        else
        {
            Debug.LogWarning("You must select both a Primary and a Secondary weapon before starting the game.");
        }
    }
}