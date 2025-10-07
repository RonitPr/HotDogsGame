using UnityEngine;

public class OfficerTrap : MonoBehaviour, IGenericTrap
{
    public bool isEffective(Ability ability)
    {
        return ability == Ability.Cute;
    }
}
