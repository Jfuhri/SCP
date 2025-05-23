using UnityEngine;

public class ChargingWeapon : MonoBehaviour
{
    public GameObject chargedProjectilePrefab;
    public Transform firePoint;
    public Camera playerCamera;

    public float chargeTimeRequired = 1.5f;
    public float projectileForce = 1000f;
    public float cooldown = 2f;
    public float bulletLifetime = 5f;

    public Animator weaponAnimator;
    public string chargingBoolParameter = "IsCharging"; // Used for charge animation only

    private float chargeTimer = 0f;
    private float lastFiredTime = 0f;
    private bool isCharging = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastFiredTime + cooldown)
        {
            StartCharging();
        }

        if (Input.GetButton("Fire1") && isCharging)
        {
            chargeTimer += Time.deltaTime;

            if (chargeTimer >= chargeTimeRequired)
            {
                Fire();
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCharging();
        }
    }

    void StartCharging()
    {
        isCharging = true;
        chargeTimer = 0f;

        if (weaponAnimator != null)
        {
            weaponAnimator.SetBool(chargingBoolParameter, true); // ?? Play charging animation
        }
    }

    void StopCharging()
    {
        isCharging = false;
        chargeTimer = 0f;

        if (weaponAnimator != null)
        {
            weaponAnimator.SetBool(chargingBoolParameter, false); // ? Stop charging animation
        }
    }

    void Fire()
    {
        isCharging = false;
        lastFiredTime = Time.time;

        if (weaponAnimator != null)
        {
            weaponAnimator.SetBool(chargingBoolParameter, false); // ? Ensure charging animation stops
        }

        GameObject projectile = Instantiate(chargedProjectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 direction = playerCamera.transform.forward;
            rb.linearVelocity = direction * projectileForce * Time.fixedDeltaTime;
            projectile.transform.forward = direction;
        }

        Destroy(projectile, bulletLifetime);
        chargeTimer = 0f;
    }
}
