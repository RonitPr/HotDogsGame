using UnityEngine;

public class OfficerEnemy : Enemy
{
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
