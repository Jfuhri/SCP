using UnityEngine;

public class RandomPatrolArea : MonoBehaviour
{
    public float patrolRadius = 30f;
    public LayerMask groundMask;
    public float minDistanceFromPlayer = 5f;

    /// <summary>
    /// Returns a random patrol point on the ground within patrolRadius of this object's position.
    /// If biasTowards is given, it biases points halfway toward that position.
    /// Ensures the point is on the ground using a downward raycast.
    /// </summary>
    /// <param name="biasTowards">Optional point to bias toward.</param>
    /// <returns>Valid patrol point on the ground or fallback to current position.</returns>
    public Vector3 GetRandomPatrolPoint(Vector3? biasTowards = null)
    {
        Vector3 basePoint = transform.position;

        // If bias target is given, move halfway towards it from this position
        if (biasTowards.HasValue)
        {
            Vector3 target = biasTowards.Value;
            // Ensure minimum distance from player to avoid clustering too close
            if (Vector3.Distance(transform.position, target) < minDistanceFromPlayer)
            {
                // Push the basePoint away along the direction from player
                Vector3 dirAway = (transform.position - target).normalized;
                basePoint = target + dirAway * minDistanceFromPlayer;
            }
            else
            {
                basePoint = Vector3.Lerp(transform.position, target, 0.5f);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * patrolRadius;
            Vector3 randomPoint = new Vector3(randomCircle.x, 0, randomCircle.y) + basePoint;

            // Raycast down to find ground height at random point
            if (Physics.Raycast(randomPoint + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f, groundMask))
            {
                return hit.point;
            }
        }

        // Fallback: return current position if no valid point found
        return transform.position;
    }
}
