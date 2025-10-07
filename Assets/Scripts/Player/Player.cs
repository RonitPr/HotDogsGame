using System;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private PlayerAttack _chokeAttack;
    [SerializeField] private Vector2 facingDirection = Vector2.right;

    public abstract Ability UseSpecialAbility();
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

        // pdate facing direction only if the player is moving
        if (inputDirection != Vector3.zero)
        {
            facingDirection = inputDirection.normalized;
        }
    }
    protected void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            DealDamage(_chokeAttack.Attack(facingDirection));
        }
    }

    private void DealDamage(RaycastHit2D[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();
            GameObject hitObject = hits[i].collider.gameObject;

            if (iDamageable != null)
            {
                Debug.Log("iDamageable found");
                if (hitObject == gameObject)
                {
                    Debug.Log("Ignoring self-hit.");
                    continue;
                }
                iDamageable.Damage(_chokeAttack.damageAmount);
            }
        }
    }

    private void UpdateAttackTransformPosition()
    {
        if (_chokeAttack.attackTransform == null) return;

        float attackDistance = _chokeAttack.attackRange * 0.75f;
        _chokeAttack.attackTransform.localPosition = facingDirection * attackDistance;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_chokeAttack.attackTransform.position, _chokeAttack.attackRange);
    }

    //Internal class that handles hitbox
    [Serializable]
    internal class PlayerAttack
    {
        [SerializeField] internal Transform attackTransform;
        [SerializeField] internal float attackRange = 1.5f;
        [SerializeField] internal LayerMask attackableLayer;
        [SerializeField] internal int damageAmount = 1;

        private RaycastHit2D[] hits;

        public RaycastHit2D[] Attack(Vector2 facingDir)
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


