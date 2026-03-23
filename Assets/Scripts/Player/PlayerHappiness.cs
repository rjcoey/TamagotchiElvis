
public class PlayerHappiness : PlayerStat
{
    protected override void Update()
    {
        if (!active) return;
        base.Update();

        if (Resource.IsHappy)
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
        PlayerEventBus.RaiseHappinessUpdated(currentValue, maxValue);
    }

    protected override void DecayStat()
    {
        base.DecayStat();
        PlayerEventBus.RaiseHappinessUpdated(currentValue, maxValue);
    }
}
