using System;
using UnityEngine;

public static class GlobalEventManager
{
    // Gunshot event
    public static event Action<Vector3> OnGunshot;

    // Call this to notify all listeners of a gunshot
    public static void ReportGunshot(Vector3 position)
    {
        OnGunshot?.Invoke(position);
    }

    // (Optional) You can add other global events here later
    // public static event Action<Vector3> OnExplosion;
    // public static void ReportExplosion(Vector3 position) => OnExplosion?.Invoke(position);
}
