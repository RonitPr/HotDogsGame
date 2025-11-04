using System;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private int _currentHitPoints;
    
    public event Action OnDefeated;

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
        OnDefeated?.Invoke();
        // Optional: play animation or disable sprite
        Destroy(gameObject, 0.3f);
    }
}
