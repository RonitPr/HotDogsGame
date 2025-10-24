using System;
using UnityEngine;


public abstract class Player : DamageTaker
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    [SerializeField] protected AttackCast _ability;
    [SerializeField] private Vector2 _facingDirection = Vector2.right;
    [SerializeField] private CameraFollow _cameraFollow;

    public float moveSpeed = 5f;
    public Rigidbody2D body;

    protected Vector3 _inputDirection;

    //animation
    public Animator Anim;
    private bool _facingLeft = true; // the assets sprites are facing left
    protected Vector3 lastMoveDirection;


    private void Awake()
    {
        GlobalHealth.CurrentHitPoints = MaxHitPoints;
    }

    void Update()
    {
        if (_cameraFollow != null && _cameraFollow.IsHovering)
            return;

        HandleMovement();
        HandleAttack();
        Animate();
        if (_inputDirection.x < 0 && !_facingLeft || _inputDirection.x > 0 && _facingLeft)
        {
            Flip();
        }
        _ability.UpdateAttackTransformPosition(_facingDirection);
    }

    public Vector3 GetInputDirection()
    {
        return _inputDirection;
    }

    private void FixedUpdate()
    {
        body.linearVelocity = _inputDirection * moveSpeed;
    }

    protected void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if((moveX == 0 && moveY == 0) && (_inputDirection.x != 0 || _inputDirection.y != 0))
        {
            lastMoveDirection = _inputDirection; // for getting the correct standing direction while idle animation plays
        }

        _inputDirection = new Vector3 (moveX, moveY);

        // Update facing direction only if the player is moving
        if (_inputDirection != Vector3.zero)
        {
            _facingDirection = _inputDirection.normalized;
        }
    }

    void Animate()
    {
        Anim.SetFloat("MoveX", _inputDirection.x);
        Anim.SetFloat("MoveY", _inputDirection.y);
        Anim.SetFloat("MoveMagnitude", _inputDirection.magnitude);
        Anim.SetFloat("LastMoveX", lastMoveDirection.x);
        Anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    void Flip()
    {
        GetComponent<SpriteRenderer>().flipX = _facingLeft;
        _facingLeft = !_facingLeft;
    }

    protected void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAbility(_ability.Cast(_facingDirection));
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
            OnPlayerDeath?.Invoke();
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_ability.attackTransform.position, _ability.attackRange);
    }
}


