using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    public float baseSpeed = 3f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;

    private float currentSpeed;
    private float verticalLookRotation = 0f;
    private CharacterController controller;
    private ArmorSystem armorSystem;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        armorSystem = GetComponent<ArmorSystem>();

        // Set speed based on armor
        if (armorSystem != null)
        {
            switch (armorSystem.equippedArmor)
            {
                case ArmorSystem.ArmorType.Light:
                    currentSpeed = armorSystem.lightSpeed;
                    break;
                case ArmorSystem.ArmorType.Medium:
                    currentSpeed = armorSystem.mediumSpeed;
                    break;
                case ArmorSystem.ArmorType.Heavy:
                    currentSpeed = armorSystem.heavySpeed;
                    break;
                default:
                    currentSpeed = baseSpeed;
                    break;
            }
        }
        else
        {
            currentSpeed = baseSpeed;
        }

        Cursor.lockState = CursorLockMode.Locked; // Lock cursor for mouse look
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX); // Horizontal look
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraTransform.localEulerAngles = Vector3.right * verticalLookRotation;

        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.SimpleMove(move * currentSpeed);
    }
}
