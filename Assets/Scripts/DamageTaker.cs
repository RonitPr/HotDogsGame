using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private int _currentHitPoints;
    [SerializeField] int _power;

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
    
    public int Power
    {
        get => _power;
        protected set => _power = value;
    }
    private void Start()
    {
        _currentHitPoints = _maxHitPoints;
    }

    // you can use the base of this function in classes that inherit from it to deal damage.
    public virtual void TakeDamage(int damagePower)
    {
        CurrentHitPoints = Mathf.Max(CurrentHitPoints - damagePower, 0);
        if (CurrentHitPoints == 0) {
            DestroyDead();
            Debug.Log("Woah it's dead!");
        }
    }

    public void ResetHitPoints()
    {
        CurrentHitPoints = MaxHitPoints;
    }

    public virtual void DestroyDead()
    {
        // gets the gameObject is is on in the inspector and destroys it
        Destroy(gameObject);
    }
}
