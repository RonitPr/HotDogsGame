using UnityEngine;
using System.Collections;

public abstract class PickupBase : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected float animationEndDelay;

    protected bool isPickedUp = false;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(animationEndDelay);
        OnPickupAnimationEnd();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPickedUp) return;

        if (other.CompareTag("Player"))
        {
            isPickedUp = true;
            animator.SetBool("isPickedUp", true);
            StartCoroutine(WaitAndPrint());
            
        }
    }

    public void OnPickupAnimationEnd()
    {
        HandlePickupEffect();   // subclass handles specific behavior
        gameObject.SetActive(false);
    }

    // Each pickup will override this
    protected abstract void HandlePickupEffect();
}
