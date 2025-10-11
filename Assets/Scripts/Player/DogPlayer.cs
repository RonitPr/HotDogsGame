using UnityEngine;

public class DogPlayer : Player
{
    [SerializeField] int _poisonPower; //todo- use poison power (or not, maybe it only slows donw the enemy instead of stunning it)

    public int PoisonPower
    {
        get => _poisonPower;
        private set => _poisonPower = value;
    }

    protected override Ability GetAbililty()
    {
        return Ability.Poison;
    }
}
