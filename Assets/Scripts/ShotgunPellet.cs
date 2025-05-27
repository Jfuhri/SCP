using UnityEngine;

public class ShotgunPellet : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damage = 5f;
    public float lifetime = 2f;
    public float collisionActivationDelay = 0.05f; // Delay before pellet can detect collisions

    private bool canCollide = false;

    void Start()
    {
        // Allow collision after a short delay to avoid self-collisions
        Invoke(nameof(EnableCollision), collisionActivationDelay);

        // Destroy the pellet after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    void EnableCollision()
    {
        canCollide = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!canCollide) return;

        // Try to get a health component on the object hit
        Health targetHealth = collision.gameObject.GetComponent<Health>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}