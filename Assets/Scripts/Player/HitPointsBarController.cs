using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

// followed this tutorial: https://www.youtube.com/watch?v=5NViMw-ALAo 
public class HitPointsBarController : MonoBehaviour
{
    public GameObject HeartPrefab;
    public GameManager GameManagerObj;
    List<HeartController> Hearts = new List<HeartController>();

    private void OnEnable()
    {
        Player.OnPlayerDamaged += DrawHearts;
    }

    private void OnDisable()
    {
        Player.OnPlayerDamaged -= DrawHearts;
    }

    private void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();
        float maxHealthRemainder = GameManagerObj.CurrentPlayer.MaxHitPoints % 2;
        int heartsToMake = (int)((GameManagerObj.CurrentPlayer.MaxHitPoints / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++) 
        {
            CreateEmptyHeart();
        }
        for (int i = 0; i < Hearts.Count; i++) 
        {
            int heartsStatusRemainder = Mathf.Clamp(GlobalHealth.CurrentHitPoints - (i*2), 0, 2); // to get the enum value 0, 1, 2
            Hearts[i].SetHeartImage((HeartStatus)heartsStatusRemainder);
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
