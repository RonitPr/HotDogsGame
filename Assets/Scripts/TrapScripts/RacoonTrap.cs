using UnityEngine;

public class RacoonTrap : FightingTrap, IPoisonable, IDamageable
{
    private void Start()
    {
        _maxArmor = 1;
        _currentArmor = _maxArmor;
    }

    public void Damage(int damageAmount)
    {
        if (IsDamageable())
        {
            TakeDamage(damageAmount);
            if (CurrentHitPoints <= 0)
            {
                DestroyDead();
            }
        }
    }

    public void Poison(int poisonPower)
    {
        if (IsPoisonable())
        {
            TakePoison(poisonPower);
        }
    }

    //public override bool isEffective(Ability ability)
    //{
    //    return ability == Ability.Poison;
    //}
}
