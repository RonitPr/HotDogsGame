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

    protected Vector2 inputDirection;

    //animation
    public Animator Anim;
    private bool _facingLeft = true; // the assets sprites are facing left
    protected Vector2 lastMoveDirection;


    private void Awake()
    {
        GlobalHealth.CurrentHitPoints = MaxHitPoints;
    }

    private void Start()
    {
        //Anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
        Animate();
        if (inputDirection.x < 0 && !_facingLeft || inputDirection.x > 0 && _facingLeft)
        {
            Flip();
        }
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

        if((moveX == 0 && moveY == 0) && (inputDirection.x != 0 || inputDirection.y != 0))
        {
            lastMoveDirection = inputDirection;
        }

        inputDirection = new Vector2 (moveX, moveY);

        // Update facing direction only if the player is moving
        if (inputDirection != Vector2.zero)
        {
            facingDirection = inputDirection.normalized;
        }
    }

    void Animate()
    {
        Anim.SetFloat("MoveX", inputDirection.x);
        Anim.SetFloat("MoveY", inputDirection.y);
        Anim.SetFloat("MoveMagnitude", inputDirection.magnitude);
        Anim.SetFloat("LastMoveX", lastMoveDirection.x);
        Anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // flips the sprite horizontally
        transform.localScale = scale;
        _facingLeft = !_facingLeft;
    }

    protected void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAbility(_ability.Cast(facingDirection));
            Anim.SetTrigger("ActiveAttack");
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


