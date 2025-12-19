using UnityEngine;

public class TV : Resource
{
    public override void ApplyEffect(PlayerStats stats)
    {
        WatchTV(stats);
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
