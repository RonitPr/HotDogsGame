using UnityEngine;

public class RacoonTrap : FightingTrap
{
    public override bool isEffective(Ability ability)
    {
        return ability == Ability.Poison;
    }
}
