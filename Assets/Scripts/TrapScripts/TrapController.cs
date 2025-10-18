using UnityEngine;

public enum TrapDirection { Horizontal, Vertical }
public enum SpriteDirection { Up, Down, Left, Right }

public class TrapController : MonoBehaviour
{
    [Header("References")]
    public GameObject visualChild;
    public BoxCollider2D blockingCollider;
    public TrapDirection direction;
    public SpriteDirection spriteDirection;
    public SpriteRenderer blockSprite;

    [Header("Sprites")]
    public Sprite horizontalSprite;
    public Sprite verticalSprite;

    private IGenericTrap trap;
    private Vector3 spriteInitialLocalPosition;

    private void Awake()
    {
        spriteInitialLocalPosition = blockingCollider.transform.localPosition;
        AdjustColliderDirection();
        
        if (visualChild == null) return;

        if (visualChild.TryGetComponent<IGenericTrap>(out var t))
        {
            trap = t;
            trap.OnDefeated += HandleDefeated;

            GameManager.Instance.RegisterTrap(trap);
        }
    }

    private void HandleDefeated()
    {
        // Remove the collider (unblocks path)
        if (blockingCollider != null)
            blockingCollider.enabled = false;

        Destroy(gameObject, 0.5f);
    }

    public void SetDirection(TrapDirection dir, SpriteDirection sdir)
    {
        direction = dir;
        spriteDirection = sdir;
        AdjustColliderDirection();
        AdjustSpriteDirection();
    }

    private void AdjustColliderDirection()
    {
        if (blockingCollider == null) return;

        if (direction == TrapDirection.Horizontal)
        {
            blockingCollider.size = new Vector2(5f, 1f);
            blockingCollider.offset = new Vector2(0, 0);
            
            if (blockSprite != null && horizontalSprite != null)
                blockSprite.sprite = horizontalSprite;
        }
        else
        {
            blockingCollider.size = new Vector2(1f, 5f);
            blockingCollider.offset = new Vector2(0, 0);
            
            if (blockSprite != null && verticalSprite != null)
                blockSprite.sprite = verticalSprite;
        }
    }
    private void AdjustSpriteDirection()
    {
        if (blockSprite == null || blockingCollider == null) return;

        Vector3 offset = Vector3.zero;

        switch (spriteDirection)
        {
            case SpriteDirection.Up:
                offset = new Vector3(0f, 1f, 0f);
                break;
            case SpriteDirection.Down:
                offset = new Vector3(0f, -1f, 0f);
                break;
            case SpriteDirection.Left:
                offset = new Vector3(-1f, 0f, 0f);
                break;
            case SpriteDirection.Right:
                offset = new Vector3(1f, 0f, 0f);
                break;
        }

        blockingCollider.transform.localPosition = spriteInitialLocalPosition + offset;
    }
}
