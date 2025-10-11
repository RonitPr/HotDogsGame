using UnityEngine;

public abstract class SimpleTrap : MonoBehaviour, IGenericTrap
{
    public virtual void GetAbilityUsedOn(Ability ability)
    {
        if (IsEffective(ability)){
            Debug.Log("Correct ability used! Disarming trap!");
            Destroy(gameObject);
        }
    }

    public abstract bool IsEffective(Ability ability);
}
