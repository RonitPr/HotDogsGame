using UnityEngine;

public class OfficerEnemy : Enemy
{
    [SerializeField]
    private float moveSpeed = 1f;

    protected override bool isAbilityEffective(string abilityName)
    {
        if (abilityName == "cute")
        {
            return true;
        }
        return false;
    }

    private void Start()
    {
        
    }
    private void Update()
    {

    }
}
