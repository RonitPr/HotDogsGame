using System;
using UnityEngine;

public class HealthPickup : PickupBase
{
    [SerializeField] private int healAmount = 1;
    
    protected override void HandlePickupEffect()
    {
        Debug.Log($"HealthPickup used — healing for {healAmount} HP!");
        GlobalHealth.AddHealthPoints(healAmount);
    }
}
