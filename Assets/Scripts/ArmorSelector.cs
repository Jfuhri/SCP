using UnityEngine;
using UnityEngine.SceneManagement;

public class ArmorSelection : MonoBehaviour
{
    public void SelectLightArmor()
    {
        PlayerPrefs.SetString("SelectedArmor", "Light");
    }

    public void SelectMediumArmor()
    {
        PlayerPrefs.SetString("SelectedArmor", "Medium");
    }

    public void SelectHeavyArmor()
    {
        PlayerPrefs.SetString("SelectedArmor", "Heavy");
    }

    public void StartGame(string sceneName)
    {
        if (PlayerPrefs.HasKey("SelectedArmor"))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("Please select an armor type before starting.");
        }
    }
}
