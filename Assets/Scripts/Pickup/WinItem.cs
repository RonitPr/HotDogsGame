using UnityEngine;

public class WinItem : PickupBase
{
    [SerializeField] private GameObject winCanvas;
    protected override void HandlePickupEffect()
    {
        Debug.Log("WinItem picked up — showing Win Canvas!");
        winCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
