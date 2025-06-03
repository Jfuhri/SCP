using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{
    public string gameOverSceneName = "GameOver"; // Set this to the name of your game over scene

    public void HandleDeath()
    {
        Debug.Log("Player has died. Loading Game Over scene...");
        SceneManager.LoadScene(gameOverSceneName);
    }
}
