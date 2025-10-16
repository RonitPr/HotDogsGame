using UnityEngine;

public class TrapSpawnPoint : MonoBehaviour
{
    public TrapDirection direction = TrapDirection.Horizontal;
    public SpriteDirection spriteDirection = SpriteDirection.Up;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.3f);

        // Draw direction arrow in Scene view
        Vector3 dir = direction == TrapDirection.Horizontal ? Vector3.right : Vector3.up;
        Gizmos.DrawLine(transform.position, transform.position + dir * 1f);
    }
}