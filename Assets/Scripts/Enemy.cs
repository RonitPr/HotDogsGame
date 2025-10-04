using UnityEngine;

public abstract class Enemy : ThingWithHitPoints
{
    protected abstract bool isAbilityEffective(string abilityName); // needs to use Enum
}
