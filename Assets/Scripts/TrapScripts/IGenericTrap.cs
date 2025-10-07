using UnityEngine;

public interface IGenericTrap
{
    // returns true if the ability is effective against the trap
    bool isEffective(Ability ability);
}
