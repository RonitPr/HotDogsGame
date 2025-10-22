using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


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

    //private float delayAfterDeath = 3f;

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
        _ability.UpdateAttackTransformPosition(_facingDirection);
    }

    private void FixedUpdate()
    {
        body.linearVelocity = _inputDirection * moveSpeed;
    }

    protected void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        _inputDirection = new Vector3 (moveX, moveY, 0);

        // Update facing direction only if the player is moving
        if (_inputDirection != Vector3.zero)
        {
            _facingDirection = _inputDirection.normalized;
        }
    }
    protected void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAbility(_ability.Cast(_facingDirection));
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
            //StartCoroutine(GoToLooseScene());
            OnPlayerDeath?.Invoke();
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_ability.attackTransform.position, _ability.attackRange);
    }

    //private IEnumerator GoToLooseScene()
    //{
    //    yield return new WaitForSeconds(delayAfterDeath);
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene("Loose");
    //}
}


