using UnityEngine;

public class WeaponLoader : MonoBehaviour
{
    private GameObject primaryWeapon;
    private GameObject secondaryWeapon;
    private GameObject activeWeapon;

    void Start()
    {
        primaryWeapon = transform.Find(GameManager.Instance.primaryWeaponName)?.gameObject;
        secondaryWeapon = transform.Find(GameManager.Instance.secondaryWeaponName)?.gameObject;

        Debug.Log($"Primary weapon: {GameManager.Instance.primaryWeaponName} found? {primaryWeapon != null}");
        Debug.Log($"Secondary weapon: {GameManager.Instance.secondaryWeaponName} found? {secondaryWeapon != null}");

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
        {
            Debug.LogWarning("SwapWeapons failed: One or both weapons not set or are the same.");
            return;
        }

        if (activeWeapon != null)
        {
            activeWeapon.SetActive(false);
        }

        activeWeapon = (activeWeapon == primaryWeapon) ? secondaryWeapon : primaryWeapon;

        if (activeWeapon != null)
        {
            activeWeapon.SetActive(true);
            Debug.Log($"Switched to: {activeWeapon.name}");
        }
        else
        {
            Debug.LogWarning("Active weapon is null after swap!");
        }
    }
}
