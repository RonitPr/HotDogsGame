using UnityEngine;

public class OfficerTrap : SimpleTrap
{
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private AnimationClip charmedAnimation;
    public override bool IsEffective(Ability ability)
    {
        return ability == Ability.Cute;
    }

    public override void DestroyTrap()
    {
        _anim.Play(charmedAnimation.name);
        Destroy(gameObject,charmedAnimation.length);
    }
}
