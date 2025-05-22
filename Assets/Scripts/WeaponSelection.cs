using UnityEngine;

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
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameplayScene);
    }
}
