using UnityEngine;

public class RacoonTrap : FightingTrap, IPoisonable, IDamageable
{
    //[SerializeField] private int _armor = 1;

    //private bool isDamageable = false;
    //private bool isPoisonable = false;

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
