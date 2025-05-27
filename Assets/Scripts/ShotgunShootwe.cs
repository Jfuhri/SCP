using UnityEngine;

public class ShotgunShooter : MonoBehaviour
{
    public GameObject pelletPrefab;
    public Transform pelletSpawnPoint;
    public Camera playerCamera;
    public int pelletCount = 8;
    public float spreadAngle = 5f; // Degrees
    public float pelletForce = 700f;
    public float shootCooldown = 1f;
    public MuzzleFlashSpawner muzzleFlashSpawner;

    private float lastShotTime = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastShotTime + shootCooldown)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 direction = GetSpreadDirection();

            GameObject pellet = Instantiate(pelletPrefab, pelletSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = pellet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction * pelletForce * Time.fixedDeltaTime;
                pellet.transform.forward = direction;
            }
            if (muzzleFlashSpawner != null)
                muzzleFlashSpawner.SpawnFlash();
        }
    }

    Vector3 GetSpreadDirection()
    {
        Vector3 baseDir = playerCamera.transform.forward;
        float spreadRadius = Mathf.Tan(spreadAngle * Mathf.Deg2Rad);
        Vector2 offset = Random.insideUnitCircle * spreadRadius;
        Vector3 spreadDir = baseDir + playerCamera.transform.right * offset.x + playerCamera.transform.up * offset.y;
        return spreadDir.normalized;
    }
}