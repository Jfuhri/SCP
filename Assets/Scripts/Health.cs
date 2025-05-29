using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth = 100f;
    [HideInInspector]
    public float armor = 0f; // This is now set by ArmorSystem

    public void TakeDamage(float amount)
    {
        // Apply armor reduction (armor is subtracted from incoming damage, clamped to minimum 0)
        float damageAfterArmor = Mathf.Max(amount - armor, 0f);

        currentHealth -= damageAfterArmor;

        Debug.Log($"{gameObject.name} took {damageAfterArmor} damage after armor ({armor}). Remaining health: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject); // Replace with death animation or logic if needed
    }
}
