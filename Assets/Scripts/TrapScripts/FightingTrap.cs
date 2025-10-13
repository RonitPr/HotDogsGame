using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class FightingTrap : DamageTaker, IGenericTrap
{
    [SerializeField]
    private int _chaseMaxDistance;
    [SerializeField]
    private float _affectTime;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _attackTimeInterval;
    [SerializeField] 
    protected AttackCast _attack;

    private Vector3 _initialPosition;
    private Vector2 _facingDirection;
    private float _timeLeftForAffect;
    private float _attackTimeCounter;
    private bool _isStunned;
    private bool _isPoisoned;
    protected bool _isInFightMode; // if not in fight mode then it is in trap mode

    public void switchToFightingMode() // might need to be protected depending on who is calling this
    {
        _isInFightMode = true;
        _attackTimeCounter = _attackTimeInterval;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isInFightMode = false;
        _isStunned = false;
        _isPoisoned = false;
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            return;
        }

        if (_isInFightMode)
        {
            if (_isStunned || _isPoisoned)
            {
                _timeLeftForAffect -= Time.deltaTime;
                if (_timeLeftForAffect <= 0.0f)
                {
                    _isStunned= false;
                    _isPoisoned= false;
                }
            }
            else
            {
                _attackTimeCounter -= Time.deltaTime;
            }

                Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            
            ChasePlayer(playerPosition);
            if(_attackTimeCounter <= 0.0f)
            {
                _attack.UpdateAttackTransformPosition(_facingDirection);
                if (!_isStunned && !_isPoisoned)
                {
                    UseAttack(_attack.Cast(_facingDirection));
                }
                _attackTimeCounter = _attackTimeInterval;
            }

            // check if player ran away far enough
            float playerDistanceFromInitialPosition = Vector3.Distance(_initialPosition, playerPosition);
            if (playerDistanceFromInitialPosition >= _chaseMaxDistance) {
                _isInFightMode = false;
                Debug.Log("You ran away...");
            }
        }
        else
        {
            if (Vector3.Distance(_initialPosition, transform.position) > 0.05f) 
            {
                GoBackToInitialPosition();
            }
        }
    }

    public virtual void GetAbilityUsedOn(Ability ability)
    {
        if (_isInFightMode)
        {
            switch (ability)
            {
                case Ability.Poison:
                    BecomePoisoned();
                    break;
                case Ability.Cute:
                    BecomeStunned();
                    break;
                case Ability.Choke:
                    TakeDamage(1);
                    break;
                default:
                    break;
            }
            _attackTimeCounter = _attackTimeInterval; // after ability is used on enemy, reset attack timer
        }
        else
        {
            // armor phase
            if (IsEffective(ability))
            {
                switchToFightingMode();
            }
        }
    }

    public abstract bool IsEffective(Ability abililty);

    private void GoBackToInitialPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, _initialPosition, 2.5f * Time.deltaTime);
    }

    private void ChasePlayer(Vector3 playerPosition)
    {
        if (!_isStunned)
        {
            float playerDistanceFromCurrentPosition = Vector3.Distance(transform.position, playerPosition);
            if(playerDistanceFromCurrentPosition > 1.5)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPosition, (_isPoisoned ? _speed * 0.25f : _speed) * Time.deltaTime);
            }
        }
    }

    public void BecomeStunned()
    {
        if (_isStunned) return;
        
        _isStunned = true;
        _timeLeftForAffect = _affectTime;

        if (_isPoisoned)
        {
            _isPoisoned = false; // timer switches to countdown for stun instead
        }
        Debug.Log($"{name} is stunned for {_timeLeftForAffect} seconds!");
    }

    public void BecomePoisoned()
    {
        if (_isPoisoned) return;

        _isPoisoned = true;
        _timeLeftForAffect = _affectTime;

        if (_isStunned)
        {
            _isStunned = false; // timer switches to countdown for poison instead
        }
        Debug.Log($"{name} is poisoned for {_timeLeftForAffect} seconds!");
    }

    protected virtual void UseAttack(RaycastHit2D[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            Player player = hits[i].collider.gameObject.GetComponent<Player>();
            GameObject hitObject = hits[i].collider.gameObject;

            if (player != null)
            {
                Debug.Log("Player attacked!");
                player.TakeDamage(1);
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attack.attackTransform.position, _attack.attackRange);
    }
}
