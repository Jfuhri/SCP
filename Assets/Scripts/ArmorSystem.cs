using UnityEngine;

public class ArmorSystem : MonoBehaviour
{
    public enum ArmorType { None, Light, Medium, Heavy }
    public ArmorType equippedArmor;

    public float lightReduction = 0.1f;
    public float mediumReduction = 0.25f;
    public float heavyReduction = 0.5f;

    public float lightSpeed = 3.5f;
    public float mediumSpeed = 2.5f;
    public float heavySpeed = 1.5f;

    public bool assignRandomArmor = false;

    void Start()
    {
        if (assignRandomArmor)
        {
            equippedArmor = (ArmorType)Random.Range(0, 4); // Includes None
        }
    }

    public float GetDamageReduction()
    {
        switch (equippedArmor)
        {
            case ArmorType.Light:
                return lightReduction;
            case ArmorType.Medium:
                return mediumReduction;
            case ArmorType.Heavy:
                return heavyReduction;
            default:
                return 0f;
        }
    }
}