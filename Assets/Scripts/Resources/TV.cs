using UnityEngine;

/// <summary>
/// A specific implementation of a Resource that represents a TV.
/// Interacting with the TV restores the player's happiness stat.
/// </summary>
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
        stats.FillHappiness(resourceFillRate);
    }

    private void StopWatchingTV()
    {
        IsHappy = false;
    }
}
