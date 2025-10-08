using UnityEngine;

public class BunnyPlayer : Player
{
    [SerializeField] int _stunDuration;
    public int StunDuration
    {
        get => _stunDuration;
        private set => _stunDuration = value;
    }

    protected override void PerformAbility(RaycastHit2D[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            IStunable iStunable = hits[i].collider.gameObject.GetComponent<IStunable>();
            GameObject hitObject = hits[i].collider.gameObject;

            if (iStunable != null)
            {
                if (hitObject == gameObject)
                {
                    continue;
                }
                iStunable.Stun(StunDuration);
            }
        }
    }
}
