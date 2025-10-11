public class RacoonTrap : FightingTrap
{
    public override bool IsEffective(Ability ability)
    {
        return ability == Ability.Poison;
    }
}
