using System;
using UnityEngine;

[Serializable]
public class AttackCast
{
    [SerializeField] internal Transform attackTransform;
    [SerializeField] internal float attackRange = 1.5f;
    [SerializeField] internal LayerMask attackableLayer;
    [SerializeField] internal int damageAmount = 1;

    private RaycastHit2D[] hits;

    public RaycastHit2D[] Cast(Vector2 facingDir)
    {
        hits = Physics2D.CircleCastAll(
            attackTransform.position,
            attackRange,
            facingDir,
            0f,
            attackableLayer
        );

        return hits;
    }
    public void UpdateAttackTransformPosition(Vector2 facingDirection)
    {
        if (attackTransform == null) return;

        float attackDistance = attackRange * 0.75f;
        attackTransform.localPosition = facingDirection * attackDistance;
    }
}
