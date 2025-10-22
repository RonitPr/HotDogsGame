using System;
using UnityEngine;
using System.Collections;

public class HandleWinItemBarricade : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float animationEndDelay = 3f;

    private void OnEnable()
    {
        CameraFollow.OnHoverAfterTrapsDefeated += DestroyBarricade;
        Debug.Log("Trigger event OnHoverAfterTrapsDefeated -> destroy barricade");
    }
    private void DestroyBarricade()
    {
        StartCoroutine(WaitAndPrint());
    }
    IEnumerator WaitAndPrint()
    {
        animator.SetBool("IsDestroyed", true);
        yield return new WaitForSeconds(animationEndDelay);
        OnBreakAnimationEnd();
    }

    private void OnBreakAnimationEnd()
    {
        Destroy(gameObject);
    }

}
