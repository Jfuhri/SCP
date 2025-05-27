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
        // Check if the object has a health script (you can replace this with your own damage logic)
        var target = collision.gameObject.GetComponent<Health>();
        if (target != null)
        {
            target.TakeDamage(damage);
        }

        Destroy(gameObject); // destroy bullet on impact
    }
}