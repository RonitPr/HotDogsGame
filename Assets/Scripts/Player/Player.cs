using System;
using UnityEngine;

public abstract class Player : DamageTaker
{
    public static event Action OnPlayerDamaged;
    //public static event Action OnPlayerDeath;

    [SerializeField] protected AttackCast _ability;
    [SerializeField] private Vector2 facingDirection = Vector2.right;

    public float moveSpeed = 5f;
    public Rigidbody2D body;

    protected Vector3 inputDirection;

    private void Awake()
    {
        GlobalHealth.CurrentHitPoints = MaxHitPoints;
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
        _ability.UpdateAttackTransformPosition(facingDirection);
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
            PerformAbility(_ability.Cast(facingDirection));
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

    public override void TakeDamage(int damagePower)
    {
        GlobalHealth.CurrentHitPoints = Mathf.Max(GlobalHealth.CurrentHitPoints - damagePower, 0);
        OnPlayerDamaged?.Invoke();
        if (GlobalHealth.CurrentHitPoints == 0)
        {
            DestroyDead();
            //OnPlayerDeath?.Invoke();
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_ability.attackTransform.position, _ability.attackRange);
    }
}


