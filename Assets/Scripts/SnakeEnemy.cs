using UnityEngine;

public class SnakeEnemy : Enemy
{
    protected override bool isAbilityEffective(string abilityName)
    {
        if (abilityName == "choke")
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
