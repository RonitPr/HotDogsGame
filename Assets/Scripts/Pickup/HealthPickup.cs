using System;
using UnityEngine;

public class HealthPickup : PickupBase
{
    [SerializeField] private int _healAmount = 1;
    
    protected override void HandlePickupEffect()
    {
        //Debug.Log($"HealthPickup used — healing for {healAmount} HP!");
        GlobalHealth.AddHealthPoints(_healAmount);
    }
}
