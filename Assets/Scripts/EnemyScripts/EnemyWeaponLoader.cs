using UnityEngine;

public class EnemyWeaponLoader : MonoBehaviour
{
    public GameObject[] weaponPrefabs; // Assign your weapon prefabs in the inspector
    public Transform weaponHoldPoint;  // Assign a hand/bone point on the enemy model

    void Start()
    {
        if (weaponPrefabs.Length == 0 || weaponHoldPoint == null)
        {
            Debug.LogWarning("EnemyWeaponLoader: Missing weapon prefabs or hold point.");
            return;
        }

        int randomIndex = Random.Range(0, weaponPrefabs.Length);
        GameObject selectedWeapon = Instantiate(weaponPrefabs[randomIndex], weaponHoldPoint);

        selectedWeapon.transform.localPosition = Vector3.zero;
        selectedWeapon.transform.localRotation = Quaternion.identity;

        EnemyWeaponData weaponData = selectedWeapon.GetComponent<EnemyWeaponData>();
        EnemyShooter enemyShooter = GetComponent<EnemyShooter>();

        if (weaponData != null && enemyShooter != null)
        {
            enemyShooter.SetWeapon(weaponData.bulletPrefab, weaponData.bulletSpawnPoint);
        }
        else
        {
            Debug.LogWarning("EnemyWeaponLoader: Missing EnemyWeaponData or EnemyShooter component.");
        }
    }
}
