using System;

public class WinItem : PickupBase
{
    public static event Action OnWinItemCollected;

    protected override void HandlePickupEffect()
    {
        OnWinItemCollected.Invoke();
    }
}

