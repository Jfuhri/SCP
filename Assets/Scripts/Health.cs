using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount, Vector3 hitOrigin)
    {
        currentHealth -= amount;

        // If it's not the player, notify any enemy AI script
        if (!CompareTag("Player"))
        {
            var shooterAI = GetComponent<EnemyShootAndMove>();
            if (shooterAI != null)
            {
                shooterAI.OnHitByPlayer(hitOrigin);
            }

            var shotgunAI = GetComponent<EnemyShotgunAndMove>();
            if (shotgunAI != null)
            {
                shotgunAI.OnHitByPlayer(hitOrigin);
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
}
