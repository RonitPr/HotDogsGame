using UnityEngine;

public class DogPlayer : Player
{
    [SerializeField] int _poisonPower;

    public int PoisonPower
    {
        get => _poisonPower;
        private set => _poisonPower = value;
    }

    protected override void PerformAbility(RaycastHit2D[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            IPoisonable iPoisonable = hits[i].collider.gameObject.GetComponent<IPoisonable>();
            GameObject hitObject = hits[i].collider.gameObject;

            if (iPoisonable != null)
            {
                if (hitObject == gameObject)
                {
                    continue;
                }
                iPoisonable.Poison(PoisonPower);
            }
        }
    }
}
