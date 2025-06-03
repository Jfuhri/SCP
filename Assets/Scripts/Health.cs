using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Example behavior: destroy enemy or reload scene for player
        if (CompareTag("Player"))
        {
            // Load game over scene for player death
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            // Destroy enemy object
            Destroy(gameObject);
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}