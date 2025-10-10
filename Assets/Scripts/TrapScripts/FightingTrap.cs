using UnityEngine;

public abstract class FightingTrap : DamageTaker, IGenericTrap
{
    [SerializeField]
    private int _chaseMaxDistance;
    [SerializeField]
    private int _stunnedTime;

    //protected bool isDamageable = true;
    protected bool isPoisonable = true;
    protected bool isStunable = true;
    protected bool isAttackable = true;

    private Vector3 _initialPosition;
    private bool _isStunned = false;
    private bool _isInFightMode = false; // if not in fight mode then it is in trap mode

    public void switchToFightingMode() // might need to be protected depending on who is calling this
    {
        _isInFightMode = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInFightMode)
        {
            if (_isStunned) {
                Debug.Log("I'm stunned!!!!");
            }
            // todo follow player

            // todo hit in front after some time interval is reached while moving

            // check if player ran away far enough
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            float currentDistance = Vector3.Distance(_initialPosition, playerPosition);
            if (currentDistance >= _chaseMaxDistance) {
                // todo go back to _initialPosition
                _isInFightMode = false;
                Debug.Log("You ran away...");
                return;
            }
        }
        // Stun is over: Re-enable movement, etc.
    }

    public bool IsDamageable()
    {
        if (!IsPoisonable() && isAttackable)
        {
            return true;
        }
        return false;
    }

    public bool IsPoisonable()
    {
        if (_currentArmor > 0)
        {
            return true;
        }
        return false;
    }

    public bool IsStunable()
    {
        if (!IsPoisonable() && !isAttackable)
        {
            return true;
        }
        return false;
    }

    //public void Stun(float duration)                      //To do: Stun
    //{
    //    if (IsStunned) return;

    //    IsStunned = true;
    //    stunTimer = duration;
    //    Debug.Log($"{name} is stunned for {duration} seconds!");

    //    // Optionally disable AI, movement, etc.
    //}
}
