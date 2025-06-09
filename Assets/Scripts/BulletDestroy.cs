using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float damage = 10f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject.GetComponent<Health>();
        if (target != null)
        {
            // Pass damage and the position of the bullet as the hit origin
            target.TakeDamage(damage, transform.position);
        }

        Destroy(gameObject);
    }
}