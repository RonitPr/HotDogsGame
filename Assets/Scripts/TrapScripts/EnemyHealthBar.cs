using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject HeartPrefab;
    public FightingTrap Enemy;
    List<HeartController> Hearts = new List<HeartController>();

    private void OnEnable()
    {
        Enemy.OnEnemyDamaged += DrawHearts;
        Enemy.OnFightModeEntered += SetAppear;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDamaged -= DrawHearts;
        Enemy.OnFightModeEntered -= SetAppear;
    }

    void Start()
    {
        //DrawHearts();
    }

    public void SetAppear()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();
        int heartsToMake = Enemy.MaxHitPoints;
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }
        for (int i = Hearts.Count; i > 0; i--)
        {
            if(Enemy.CurrentHitPoints < i)
            {
                Hearts[i - 1].SetHeartImage(HeartStatus.Empty);
            }
            else
            {
                Hearts[i - 1].SetHeartImage(HeartStatus.Full);
            }
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(HeartPrefab);
        newHeart.transform.SetParent(transform);

        HeartController heart = newHeart.GetComponent<HeartController>();
        heart.SetHeartImage(HeartStatus.Empty);
        Hearts.Add(heart);
    }

    public void ClearHearts() // reset all heart objects in the canvas
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        Hearts = new List<HeartController>();
    }
}
