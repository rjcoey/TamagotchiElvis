using UnityEngine;

public class TV : Resource
{
    public override void Use()
    {
        WatchTV();
    }

    public override void StopUsing()
    {
        StopWatchingTV();
    }

    private void WatchTV()
    {
        if (!IsHappy)
        {
            IsHappy = true;
        }
    }

    private void StopWatchingTV()
    {
        IsHappy = false;
    }
}
