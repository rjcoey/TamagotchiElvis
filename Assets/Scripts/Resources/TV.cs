using UnityEngine;

public class TV : Resource
{
    public override void Use(PlayerStats playerStats)
    {
        WatchTV(playerStats);
    }

    public override void StopUsing()
    {
        StopWatchingTV();
    }

    private void WatchTV(PlayerStats stats)
    {
        if (!IsHappy)
        {
            IsHappy = true;
        }
        stats.IncreaseHappiness(resourceFillRate * Time.deltaTime);
    }

    private void StopWatchingTV()
    {
        IsHappy = false;
    }
}
