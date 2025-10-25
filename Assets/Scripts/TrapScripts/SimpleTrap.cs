using System;
using UnityEngine;

public abstract class SimpleTrap : MonoBehaviour, IGenericTrap
{
    public event Action OnDefeated;

    private Animator _anim;
    [SerializeField]
    private AnimationClip effectiveAnimation; // animation used when the correct ability was used on this trap
    
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public virtual void GetAbilityUsedOn(Ability ability)
    {
        if (IsEffective(ability)){
            OnDefeated?.Invoke();
            Debug.Log($"Trap Defeat called for {this} (hash {GetHashCode()})");
            DestroyTrap();
        }
    }

    public abstract bool IsEffective(Ability ability);

    public virtual void DestroyTrap()
    {
        _anim.Play(effectiveAnimation.name);
        Destroy(gameObject, effectiveAnimation.length);
    }
}
