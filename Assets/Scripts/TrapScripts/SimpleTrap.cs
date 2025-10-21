using System;
using UnityEngine;

public abstract class SimpleTrap : MonoBehaviour, IGenericTrap
{
    public event Action OnDefeated;

    public virtual void GetAbilityUsedOn(Ability ability)
    {
        if (IsEffective(ability)){
            OnDefeated?.Invoke();
            Debug.Log("Correct ability used! Disarming trap!");
            DestroyTrap();
        }
    }

    public abstract bool IsEffective(Ability ability);

    public virtual void DestroyTrap()
    {
        Destroy(gameObject);
    }
}
