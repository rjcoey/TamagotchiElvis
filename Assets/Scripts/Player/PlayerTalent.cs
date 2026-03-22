
public class PlayerTalent : PlayerStat
{
    void Update()
    {
        if (!active) return;

        if (Resource.IsPractising)
        {
            IncreaseStat();
        }
        else
        {
            DecayStat();
        }
    }

    protected override void IncreaseStat()
    {
        base.IncreaseStat();
        PlayerEventBus.RaiseTalentUpdated(currentValue, maxValue);
    }

    protected override void DecayStat()
    {
        base.DecayStat();
        PlayerEventBus.RaiseTalentUpdated(currentValue, maxValue);
    }
}
