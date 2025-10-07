using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    [SerializeField]
    public int MaxHitPoints {  get; private set; }
    public int CurrentHitPoints { get; private set; }

    // you can use the base of this function in classes that inherit from it to deal damage.
    public virtual void TakeDamage(int damagePower)
    {
        CurrentHitPoints = Mathf.Min(CurrentHitPoints - damagePower, 0);
    }

    public void ResetHitPoints()
    {
        CurrentHitPoints = MaxHitPoints;
    }

    public void DestroyDead()
    {
        // gets the gameObject is is on in the inspector and destroys it
        Destroy(gameObject);
    }
}
