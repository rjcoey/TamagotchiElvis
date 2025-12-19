using UnityEngine;

public class Guitar : Resource
{
    public override void ApplyEffect(PlayerStats stats)
    {
        PractiseGuitar(stats);
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
