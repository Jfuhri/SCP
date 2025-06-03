using UnityEngine;
using UnityEngine.AI;

public class EnemyShootAndMove : MonoBehaviour
{
    [Header("Combat")]
    public float shootingRange = 10f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Vision")]
    public float viewAngle = 120f;
    public float viewDistance = 25f;
    public LayerMask lineOfSightMask;

    [Header("Patrol")]
    public float patrolRadius = 30f;
    public float patrolWaitTime = 3f;

    private Transform player;
    private NavMeshAgent agent;
    private float nextFireTime;
    private float patrolWaitTimer;
    private Vector3 currentPatrolTarget;
    private bool isPatrolling = true;
    private Vector3 lastKnownPlayerPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        lastKnownPlayerPosition = transform.position;  // Initialize lastKnownPlayerPosition to own position
        SetNewPatrolPoint();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= shootingRange && IsInFieldOfView() && HasLineOfSight())
        {
            // Attack mode
            agent.isStopped = true;
            FacePlayer();
            lastKnownPlayerPosition = player.position;

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else if (IsInFieldOfView() && HasLineOfSight())
        {
            // Player seen but not in range – chase
            agent.isStopped = false;
            agent.SetDestination(player.position);
            lastKnownPlayerPosition = player.position;
            isPatrolling = false;
        }
        else
        {
            // Patrol mode
            PatrolBehavior();
        }
    }

    void PatrolBehavior()
    {
        if (!isPatrolling)
        {
            // Just switched to patrol
            isPatrolling = true;
            patrolWaitTimer = 0f;
            SetNewPatrolPoint();
        }

        agent.isStopped = false;

        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            patrolWaitTimer += Time.deltaTime;
            if (patrolWaitTimer >= patrolWaitTime)
            {
                SetNewPatrolPoint();
                patrolWaitTimer = 0f;
            }
        }
    }

    void SetNewPatrolPoint()
    {
        Vector3 basePoint = (Random.value > 0.6f) ? lastKnownPlayerPosition : transform.position;
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection.y = 0f; // Flatten to ground level

        Vector3 candidatePoint = basePoint + randomDirection;

        if (NavMesh.SamplePosition(candidatePoint, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
        {
            currentPatrolTarget = hit.position;
            agent.SetDestination(currentPatrolTarget);
        }
        else
        {
            // Fallback: stay in place if no valid point found
            currentPatrolTarget = transform.position;
            agent.SetDestination(currentPatrolTarget);
        }
    }

    bool HasLineOfSight()
    {
        Vector3 direction = (player.position + Vector3.up * 1f) - firePoint.position;
        if (Physics.Raycast(firePoint.position, direction.normalized, out RaycastHit hit, viewDistance, lineOfSightMask))
        {
            return hit.transform.CompareTag("Player");
        }
        return false;
    }

    bool IsInFieldOfView()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle <= viewAngle / 2f && directionToPlayer.magnitude <= viewDistance;
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void Shoot()
    {
        if (bulletPrefab && firePoint)
        {
            Vector3 direction = (player.position - firePoint.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Instantiate(bulletPrefab, firePoint.position, lookRotation);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (player == null) return;

        Gizmos.color = Color.cyan;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftBoundary * viewDistance);
        Gizmos.DrawRay(transform.position, rightBoundary * viewDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, player.position);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(currentPatrolTarget, 1f);
    }
}
