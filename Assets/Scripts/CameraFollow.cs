using UnityEngine;
using System.Collections;
using System;

public class CameraFollow : MonoBehaviour
{
    public static event Action OnHoverStarted;
    public static event Action OnHoverEnded;
    public static event Action OnHoverAfterTrapsDefeated;
    public bool IsHovering => _isHovering;

    [Header("Follow Settings")]
    public Transform target;     // Who to follow (your active player)
    public float smoothSpeed = 1f;
    public Vector3 offset;       // Camera offset (so it’s not right on top of the player)
    public float minX, maxX, minY, maxY;

    [Header("Cinematic Settings")]
    [SerializeField] private Transform _winPickup;
    [SerializeField] private float _hoverSpeed = 5f;   // Speed of camera movement during hover
    [SerializeField] private float _hoverHoldTime = 2f; // Time to stay over WinPickup
    [SerializeField] private float _hoverHoldTimeAfterTrapsDefeated = 8f; // Time to stay over WinPickup
    [SerializeField] private float _returnSpeed = 2f;  // Speed when returning to player

    private Vector3 _speed;
    private bool _isHovering = false;
    public bool isHoveringAfterTrapsDefeated = false;

    private void Start()
    {
        if (_winPickup != null)
            StartCoroutine(HoverOverWinItem());
    }

    void LateUpdate()
    {
        if (_isHovering || target == null) return;

        Vector3 desiredPosition = target.position + offset;
        // Clamp the desired position within the defined boundaries, Z-axis is for 3D games
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _speed, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private IEnumerator HoverOverWinItem()
    {
        _isHovering = true;

        Vector3 startPos = transform.position;
        Vector3 targetPos = _winPickup.position + new Vector3(0, 0, offset.z); // maintain camera Z offset

        yield return StartCoroutine(MoveCamera(startPos, targetPos, _hoverSpeed));

        if (isHoveringAfterTrapsDefeated == true)
        {
            Debug.Log("Entered if loop isHoveringAfterTrapsDefeated");
            yield return new WaitForSeconds(_hoverHoldTimeAfterTrapsDefeated);
            OnHoverAfterTrapsDefeated?.Invoke();
            yield return new WaitForSeconds(_hoverHoldTime);
        }
        else
        {
            Debug.Log("Entered if loop Normal Hover beforeTrapsDefeated");
            OnHoverStarted?.Invoke();
            yield return new WaitForSeconds(_hoverHoldTime);
        }
        
        OnHoverEnded?.Invoke();

        Vector3 returnPos = target.position + offset;
        yield return StartCoroutine(MoveCamera(transform.position, returnPos, _returnSpeed));

        _isHovering = false;
    }

    private IEnumerator MoveCamera(Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration); // smooth easing
            transform.position = Vector3.Lerp(from, to, t);
            yield return null;
        }
    }

    private void OnEnable()
    {
        GameManager.OnAllTrapsDefeated += HoverOnAllTrapsDefeated;
    }

    private void HoverOnAllTrapsDefeated()
    {
        isHoveringAfterTrapsDefeated = true;
        if (_winPickup != null)
            StartCoroutine(HoverOverWinItem());
    }
}
