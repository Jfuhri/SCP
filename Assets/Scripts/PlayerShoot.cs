using UnityEngine;

public class FirstPersonShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletForce = 700f;
    public Camera playerCamera;
    public float shootCooldown = 0.5f;
    public bool isAutomatic = false; //  Set this in the inspector or from weapon data

    private float lastShotTime = 0f;

    void Update()
    {
        bool firePressed = isAutomatic ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");

        if (firePressed && Time.time >= lastShotTime + shootCooldown)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    void Shoot()
    {
        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Align bullet with shooting direction
        Vector3 shootDirection = playerCamera.transform.forward;

        // Apply velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bulletForce * Time.fixedDeltaTime;
            bullet.transform.forward = shootDirection;
        }
    }
}