using System;
using UnityEngine;
using System.Collections;

public class HandleWinItemBarricade : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _animationEndDelay = 3f;

    private void OnEnable()
    {
        CameraFollow.OnHoverAfterTrapsDefeated += DestroyBarricade;
    }

    private void OnDisable()
    {
        CameraFollow.OnHoverAfterTrapsDefeated -= DestroyBarricade;
    }

    private void DestroyBarricade()
    {
        StartCoroutine(WaitAndPrint());
    }
    IEnumerator WaitAndPrint()
    {
        _animator.SetBool("IsDestroyed", true);
        yield return new WaitForSeconds(_animationEndDelay);
        OnBreakAnimationEnd();
    }

    private void OnBreakAnimationEnd()
    {
        Destroy(gameObject);
    }

}
