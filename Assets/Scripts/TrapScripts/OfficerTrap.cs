using UnityEngine;

public class OfficerTrap : SimpleTrap
{
    public override bool IsEffective(Ability ability)
    {
        return ability == Ability.Cute;
    }
}
