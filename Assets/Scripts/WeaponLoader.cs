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
            Quaternion lookRot = Quaternion.LookRotation(Camera.main.transform.forward);

            // Get offset if it exists
            WeaponInfo info = activeWeapon.GetComponent<WeaponInfo>();
            Quaternion offsetRot = Quaternion.Euler(info != null ? info.rotationOffsetEuler : Vector3.zero);

            activeWeapon.transform.rotation = Quaternion.Lerp(
                activeWeapon.transform.rotation,
                lookRot * offsetRot,
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
