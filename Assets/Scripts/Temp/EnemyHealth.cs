using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0 )
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
