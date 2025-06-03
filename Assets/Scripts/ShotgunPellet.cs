using UnityEngine;

public class ShotgunPellet : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damage = 5f;
    public float lifetime = 2f;
    public float collisionActivationDelay = 0.05f; // Delay to prevent self-collision

    private bool canCollide = false;

    void Start()
    {
        // Delay collision detection to avoid hitting the shooter
        Invoke(nameof(EnableCollision), collisionActivationDelay);

        // Destroy the pellet after it has existed long enough
        Destroy(gameObject, lifetime);
    }

    void EnableCollision()
    {
        canCollide = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!canCollide) return;

        // Attempt to damage object hit
        if (collision.gameObject.TryGetComponent<Health>(out var targetHealth))
        {
            targetHealth.TakeDamage(damage);
        }

        // Optional: Add impact effects here (e.g., sparks, blood, sound)

        Destroy(gameObject);
    }
}
