using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // Who to follow (your active player)
    public float smoothSpeed = 1f;
    public Vector3 offset;       // Camera offset (so it’s not right on top of the player)

    private Vector3 speed;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref speed, smoothSpeed);
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
