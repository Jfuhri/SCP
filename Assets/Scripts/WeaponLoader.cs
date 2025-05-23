using UnityEngine;

public class WeaponLoader : MonoBehaviour
{
    private GameObject primaryWeapon;
    private GameObject secondaryWeapon;
    private GameObject activeWeapon;

    void Start()
    {
        // Find weapons by name among children
        primaryWeapon = transform.Find(GameManager.Instance.primaryWeaponName)?.gameObject;
        secondaryWeapon = transform.Find(GameManager.Instance.secondaryWeaponName)?.gameObject;

        if (primaryWeapon != null)
        {
            primaryWeapon.SetActive(true);
            activeWeapon = primaryWeapon;
        }

        if (secondaryWeapon != null && secondaryWeapon != primaryWeapon)
        {
            secondaryWeapon.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapWeapons();
        }

        if (activeWeapon != null && Camera.main != null)
        {
            // Get base rotation toward camera
            Quaternion lookRot = Quaternion.LookRotation(Camera.main.transform.forward);

            // Apply correction — adjust as needed for your model
            Quaternion modelOffset = Quaternion.Euler(0, 90, 0); // Example: rotates model to face camera's forward
            activeWeapon.transform.rotation = Quaternion.Lerp(
                activeWeapon.transform.rotation,
                lookRot * modelOffset,
                Time.deltaTime * 10f
            );
        }
    }

    void SwapWeapons()
    {
        if (primaryWeapon == null || secondaryWeapon == null || primaryWeapon == secondaryWeapon)
            return;

        activeWeapon.SetActive(false);
        activeWeapon = (activeWeapon == primaryWeapon) ? secondaryWeapon : primaryWeapon;
        activeWeapon.SetActive(true);
    }
}
