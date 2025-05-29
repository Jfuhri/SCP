using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : MonoBehaviour
{
    private GameObject bulletPrefab;
    private Transform bulletSpawnPoint;

    public float bulletForce = 700f;
    public float shootCooldown = 1f;
    public float shootingRange = 10f;
    public float detectionRange = 20f;
    public Transform player;

    private float lastShotTime = 0f;
    private NavMeshAgent agent;

    void Start()
    {
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null || bulletPrefab == null || bulletSpawnPoint == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);
        }

        if (distanceToPlayer <= shootingRange)
        {
            agent.isStopped = true;

            // Aim
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            if (Time.time >= lastShotTime + shootCooldown)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }
        else
        {
            agent.isStopped = false;
        }
    }

    public void SetWeapon(GameObject bulletPrefab, Transform bulletSpawnPoint)
    {
        this.bulletPrefab = bulletPrefab;
        this.bulletSpawnPoint = bulletSpawnPoint;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = bulletSpawnPoint.forward * bulletForce * Time.fixedDeltaTime;
        }
    }
}
