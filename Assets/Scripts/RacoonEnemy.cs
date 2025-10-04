using UnityEngine;

public class RacoonEnemy : Enemy
{
    protected override bool isAbilityEffective(string abilityName)
    {
        if (abilityName == "poison")
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
