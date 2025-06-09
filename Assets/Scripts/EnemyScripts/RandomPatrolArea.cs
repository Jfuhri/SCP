using UnityEngine;
using UnityEngine.AI;

public class RandomPatrolArea : MonoBehaviour
{
    public float patrolRadius = 30f;
    public LayerMask groundMask;
    public float minDistanceFromPlayer = 5f;
    public bool useNavMesh = true;

    // Debug info
    private Vector3? lastBiasPoint;
    private Vector3? lastPatrolPoint;

    /// <summary>
    /// Returns a random patrol point within patrolRadius of this object's position.
    /// Applies a dynamic bias toward the player's last known location depending on patrolBias (0–1).
    /// Ensures point is walkable and not too close to player.
    /// </summary>
    public Vector3 GetRandomPatrolPoint(Vector3? biasTowards = null, float patrolBias = 0f)
    {
        Vector3 basePoint = transform.position;
        lastBiasPoint = null;

        if (biasTowards.HasValue)
        {
            Vector3 target = biasTowards.Value;
            lastBiasPoint = target;

            // Move the base point toward the player by patrolBias percent
            basePoint = Vector3.Lerp(transform.position, target, Mathf.Clamp01(patrolBias));

            // Prevent clustering too close
            if (Vector3.Distance(basePoint, target) < minDistanceFromPlayer)
            {
                Vector3 dir = (basePoint - target).normalized;
                basePoint = target + dir * minDistanceFromPlayer;
            }
        }

        // Try to find a valid patrol point
        for (int i = 0; i < 10; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * patrolRadius;
            Vector3 candidate = new Vector3(randomCircle.x, 0, randomCircle.y) + basePoint;

            // Sample point on ground using raycast
            if (Physics.Raycast(candidate + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f, groundMask))
            {
                Vector3 finalPoint = hit.point;

                // Optionally confirm point is on NavMesh
                if (useNavMesh && NavMesh.SamplePosition(finalPoint, out NavMeshHit navHit, 2f, NavMesh.AllAreas))
                {
                    lastPatrolPoint = navHit.position;
                    Debug.Log($"{gameObject.name} generated patrol point at {navHit.position} (bias: {lastBiasPoint})");
                    return navHit.position;
                }
                else if (!useNavMesh)
                {
                    lastPatrolPoint = finalPoint;
                    return finalPoint;
                }
            }
        }

        // Fallback: stay in place
        lastPatrolPoint = transform.position;
        return transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        if (lastBiasPoint.HasValue)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(lastBiasPoint.Value, 1.5f);
            Gizmos.DrawLine(transform.position, lastBiasPoint.Value);
        }

        if (lastPatrolPoint.HasValue)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(lastPatrolPoint.Value, 0.7f);
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
}
