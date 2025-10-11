using UnityEngine;

public interface IGenericTrap
{
    // returns true if the ability is effective against the trap
    bool IsEffective(Ability ability);

    void GetAbilityUsedOn(Ability ability);
}