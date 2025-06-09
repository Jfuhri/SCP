using UnityEngine;

public static class GlobalEventManager
{
    public static System.Action<Vector3, Object> OnGunshot;

    public static void RaiseGunshot(Vector3 position, Object source)
    {
        OnGunshot?.Invoke(position, source);
    }
}