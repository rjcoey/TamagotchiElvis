using UnityEngine;

public class Guitar : Resource
{
    public override void Use(PlayerStats playerStats)
    {
        PractiseGuitar(playerStats);
    }

    public override void StopUsing()
    {
        StopPractising();
    }

    private void PractiseGuitar(PlayerStats stats)
    {
        if (!IsPractising)
        {
            IsPractising = true;
        }
        stats.IncreaseTalent(resourceFillRate * Time.deltaTime);
    }

    private void StopPractising()
    {
        IsPractising = false;
    }
}
