using UnityEngine;

public class BullSimpleTrap : SimpleTrap
{
    public override bool IsEffective(Ability ability)
    {
        return ability == Ability.Choke;
    }
}
