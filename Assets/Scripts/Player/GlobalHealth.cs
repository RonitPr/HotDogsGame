using System;
using UnityEngine;

public static class GlobalHealth
{
    public static int CurrentHitPoints;
    public static event Action OnPickedHealth;

    public static void AddHealthPoints(int pointsToAdd)
    {
        CurrentHitPoints += pointsToAdd;
        OnPickedHealth?.Invoke();
    }
}
