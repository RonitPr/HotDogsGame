public class RaccoonTrap : FightingTrap
{
    public override bool IsEffective(Ability ability)
    {
        return ability == Ability.Poison;
    }
}
