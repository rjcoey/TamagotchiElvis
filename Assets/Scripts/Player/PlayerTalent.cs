
public class PlayerTalent : PlayerStat
{
    protected override void Update()
    {
        if (!active) return;
        base.Update();

        if (Resource.IsPractising)
        {
            ImproveStat();
        }
        else
        {
            DecayStat();
        }
    }

    protected override void ImproveStat()
    {
        base.ImproveStat();
        PlayerEventBus.RaiseTalentUpdated(currentValue, maxValue);
    }

    protected override void DecayStat()
    {
        base.DecayStat();
        PlayerEventBus.RaiseTalentUpdated(currentValue, maxValue);
    }
}
