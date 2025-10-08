using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private int _currentHitPoints;
    [SerializeField] protected int _maxArmor;
    [SerializeField] protected int _currentArmor;

    public int MaxHitPoints
    {
        get => _maxHitPoints;
        protected set => _maxHitPoints = value;
    }

    public int CurrentHitPoints
    {
        get => _currentHitPoints;
        protected set => _currentHitPoints = value;
    }

    public int Armor
    {
        get => _maxArmor;
        protected set => _maxArmor = value;
    }

    public int CurrentArmor
    {
        get => _maxArmor;
        protected set => _currentArmor = value;
    }

    // you can use the base of this function in classes that inherit from it to deal damage.
    public virtual void TakeDamage(int damagePower)
    {
        CurrentHitPoints = Mathf.Max(CurrentHitPoints - damagePower, 0);
    }

    public virtual void TakePoison(int poisonPower)
    {
        CurrentArmor = Mathf.Max(CurrentArmor - poisonPower, 0);
    }



    public void ResetHitPoints()
    {
        CurrentHitPoints = MaxHitPoints;
    }

    public void DestroyDead()
    {
        // gets the gameObject is is on in the inspector and destroys it
        Destroy(gameObject);
    }
}
