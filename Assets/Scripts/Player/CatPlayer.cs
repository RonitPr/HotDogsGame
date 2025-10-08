using UnityEngine;

public class CatPlayer : Player
{
    protected override void PerformAbility(RaycastHit2D[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();
            GameObject hitObject = hits[i].collider.gameObject;

            if (iDamageable != null)
            {
                Debug.Log("iDamageable found");
                if (hitObject == gameObject)
                {
                    Debug.Log("Ignoring self-hit.");
                    continue;
                }
                iDamageable.Damage(Power);
            }
        }
    }
}
