using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // Who to follow (your active player)
    public float smoothSpeed = 1f;
    public Vector3 offset;       // Camera offset (so it’s not right on top of the player)
    public float minX, maxX, minY, maxY;

    private Vector3 _speed;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        // Clamp the desired position within the defined boundaries, Z-axis is for 3D games
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _speed, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
