using System;
using UnityEngine;

public abstract class Player : DamageTaker
{
    [SerializeField] protected PlayerAttack _ability;
    [SerializeField] private Vector2 facingDirection = Vector2.right;

    public float moveSpeed = 5f;
    public Rigidbody2D body;

    protected Vector3 inputDirection;

    void Update()
    {
        HandleMovement();
        HandleAttack();
        UpdateAttackTransformPosition();
    }

    private void FixedUpdate()
    {
        body.linearVelocity = inputDirection * moveSpeed;
    }

    protected void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        inputDirection = new Vector3 (moveX, moveY, 0);

        // Update facing direction only if the player is moving
        if (inputDirection != Vector3.zero)
        {
            facingDirection = inputDirection.normalized;
        }
    }
    protected void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAbility(_ability.CastAbility(facingDirection));
        }
    }

    protected virtual void PerformAbility(RaycastHit2D[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            IGenericTrap trap = hits[i].collider.gameObject.GetComponent<IGenericTrap>();
            GameObject hitObject = hits[i].collider.gameObject;

            if (trap != null)
            {
                Debug.Log("IGenericTrap found");
                trap.GetAbilityUsedOn(GetAbililty());
            }
        }
    }

    protected abstract Ability GetAbililty();

    private void UpdateAttackTransformPosition()
    {
        if (_ability.attackTransform == null) return;

        float attackDistance = _ability.attackRange * 0.75f;
        _ability.attackTransform.localPosition = facingDirection * attackDistance;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_ability.attackTransform.position, _ability.attackRange);
    }

    //Internal class that handles hitbox
    [Serializable]
    protected class PlayerAttack
    {
        [SerializeField] internal Transform attackTransform;
        [SerializeField] internal float attackRange = 1.5f;
        [SerializeField] internal LayerMask attackableLayer;
        [SerializeField] internal int damageAmount = 1;

        private RaycastHit2D[] hits;

        public RaycastHit2D[] CastAbility(Vector2 facingDir)
        {
            hits = Physics2D.CircleCastAll(
                attackTransform.position,
                attackRange,
                facingDir, //(changed) Vector2.right,
                0f,
                attackableLayer
            );

            return hits;
        }
    }
}


