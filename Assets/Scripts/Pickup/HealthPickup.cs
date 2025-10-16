using UnityEngine;

public class HealthPickup : PickupBase
{
    [SerializeField] private int healAmount = 1;

    protected override void HandlePickupEffect()
    {
        Debug.Log($"HealthPickup used — healing for {healAmount} HP!");
        // Example — if you have a PlayerHealth singleton:
        // PlayerHealth.Instance.Heal(healAmount);
        GlobalHealth.CurrentHitPoints += healAmount;
    }
}
