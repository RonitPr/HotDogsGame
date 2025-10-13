using UnityEngine;

public enum TrapDirection { Horizontal, Vertical }

public class TrapController : MonoBehaviour
{
    [Header("References")]
    public GameObject visualChild;
    public BoxCollider2D blockingCollider;
    public TrapDirection direction;

    //private DamageTaker fightingTrap;
    //private SimpleTrap simpleTrap;
    private IGenericTrap trap;

    private void Awake()
    {
        AdjustColliderDirection();
        //if (visualChild != null)
        //    fightingTrap = visualChild.GetComponent<DamageTaker>();
        //if (visualChild != null)
        //    simpleTrap = visualChild.GetComponent<SimpleTrap>();
        if (visualChild == null) return;
        if (visualChild.TryGetComponent<IGenericTrap>(out var t))
        {
            trap = t;
            trap.OnDefeated += HandleDefeated;
        }
    }

    private void Start()
    {
        // Register callback so TrapEnemy notifies when defeated
        //if (fightingTrap != null)
        //    fightingTrap.OnDefeated += HandleDefeated;
        //if (simpleTrap != null)
        //    simpleTrap.OnDefeated += HandleDefeated;
    }

    private void HandleDefeated()
    {
        // Optional: play particle or sound
        Debug.Log($"{name} defeated — path unblocked!");

        // Remove the collider (unblocks path)
        if (blockingCollider != null)
            blockingCollider.enabled = false;

        Destroy(gameObject, 0.5f);
    }

    public void SetDirection(TrapDirection dir)
    {
        direction = dir;
        AdjustColliderDirection();
        //transform.rotation = direction == TrapDirection.Horizontal ? Quaternion.identity : Quaternion.Euler(0, 0, 90); \\Set sprite direction
    }

    private void AdjustColliderDirection()
    {
        if (blockingCollider == null) return;

        //switch (direction)
        //{
        //    case TrapDirection.Horizontal:
        //        blockingCollider.size = new Vector2(5f, 1f);
        //        blockingCollider.offset = Vector2.zero;
        //        transform.rotation = Quaternion.identity;
        //        Debug.Log("I'm Horizontal");
        //        break;

        //    case TrapDirection.Vertical:
        //        blockingCollider.size = new Vector2(1f, 5f);
        //        blockingCollider.offset = Vector2.zero;
        //        transform.rotation = Quaternion.identity; // keep upright
        //        Debug.Log("I'm Vertical");
        //        break;
        //}

        if (blockingCollider == null)
            return;

        if (direction == TrapDirection.Horizontal)
        {
            blockingCollider.size = new Vector2(5f, 1f);
            blockingCollider.offset = new Vector2(0, 0);
        }
        else
        {
            blockingCollider.size = new Vector2(1f, 5f);
            blockingCollider.offset = new Vector2(0, 0);
        }
    }
}
