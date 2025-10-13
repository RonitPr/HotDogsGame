using System;
using UnityEngine;

public interface IGenericTrap
{
    event Action OnDefeated;

    // returns true if the ability is effective against the trap
    bool IsEffective(Ability ability);

    void GetAbilityUsedOn(Ability ability);
}