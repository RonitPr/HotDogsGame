using UnityEngine;

public class BunnyPlayer : Player
{
    [SerializeField] int _stunDuration;
    public int StunDuration // todo- use stun duration
    {
        get => _stunDuration;
        private set => _stunDuration = value;
    }

    protected override Ability GetAbililty()
    {
        return Ability.Cute;
    }
}
