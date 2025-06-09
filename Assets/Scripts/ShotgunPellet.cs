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
        // Prevent early collision with shooter
        Invoke(nameof(EnableCollision), collisionActivationDelay);

        // Auto-destroy after its lifespan
        Destroy(gameObject, lifetime);
    }

    void EnableCollision()
    {
        canCollide = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!canCollide) return; // ADD THIS CHECK

        var target = collision.gameObject.GetComponent<Health>();
        if (target != null)
        {
            target.TakeDamage(damage, transform.position);
        }
        Destroy(gameObject);
    }

}
