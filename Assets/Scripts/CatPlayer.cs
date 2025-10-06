using UnityEngine;

public class CatPlayer : Player
{
    public override Ability UseSpecialAbility() => Ability.Choke;
}
