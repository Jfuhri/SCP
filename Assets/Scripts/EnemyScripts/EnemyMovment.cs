using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyMovement : MonoBehaviour
{
    public float baseSpeed = 3f;
    private float currentSpeed;
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
    }

    void Update()
    {
        Vector3 move = transform.forward;
        controller.SimpleMove(move * currentSpeed);
    }
}
