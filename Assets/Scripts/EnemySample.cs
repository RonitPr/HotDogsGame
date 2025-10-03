using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Ability Weakness;

    public void TakeDamage(Player attacker)
    {
        if (attacker.UseSpecialAbility() == Weakness)       //change name convention to: GetAbility() ??
        {
            Debug.Log("Enemy defeated!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("That ability doesn’t work!");
        }
    }
}

