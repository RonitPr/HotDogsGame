using UnityEngine;

public class CatPlayer : Player
{
    protected override Ability GetAbililty()
    {
        return Ability.Choke;
    }
}
