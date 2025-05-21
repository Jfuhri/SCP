using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Optional: destroy on collision
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
