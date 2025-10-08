using UnityEngine;

public class OfficerTrap : MonoBehaviour//, IGenericTrap    ->I think we can merge it with fighting trap. I'll explain more verbally.
{
    public bool isEffective(Ability ability)
    {
        return ability == Ability.Cute;
    }

}
