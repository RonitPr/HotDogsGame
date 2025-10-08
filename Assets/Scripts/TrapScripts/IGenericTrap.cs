using UnityEngine;

public interface IGenericTrap
{
    // returns true if the ability is effective against the trap
    //bool isEffective(Ability ability);
    bool IsDamageable();
    bool IsPoisonable();
    bool IsStunable();
}
//Can move all to Interfaces Stun Poison Damageable, and emit IGenericTrap ->what do you think