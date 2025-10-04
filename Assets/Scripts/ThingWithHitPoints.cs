using UnityEngine;

public class ThingWithHitPoints : MonoBehaviour
{
    [SerializeField]
    private int _maxHitPoints = 3;
    private int _currentHitPoints;

    public ThingWithHitPoints()
    {
        _currentHitPoints = _maxHitPoints;
    }

    public void TakeDamage()
    {
        _currentHitPoints--;
    }

    public void DealDamage(ThingWithHitPoints other)
    {
        other.TakeDamage();
    }

    public bool GetIsDead()
    {
        return _currentHitPoints <= 0;
    }

    public void Reset()
    {
        _currentHitPoints = _maxHitPoints;
    }
}
