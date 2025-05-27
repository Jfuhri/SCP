using UnityEngine;

public class MuzzleFlashSpawner : MonoBehaviour
{
    [Header("Muzzle Flash Settings")]
    public GameObject muzzleFlashPrefab;
    public Transform muzzlePoint;
    public float flashLifetime = 0.2f;

    public void SpawnFlash()
    {
        if (muzzleFlashPrefab != null && muzzlePoint != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation, muzzlePoint);
            Destroy(flash, flashLifetime);
        }
        else
        {
            Debug.LogWarning("Muzzle flash prefab or muzzle point is not assigned.");
        }
    }
}