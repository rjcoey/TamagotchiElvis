public class PlayerHunger : PlayerStat
{
    protected override void Update()
    {
        if (!active) return;
        base.Update();

        if (Resource.IsEating)
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
        PlayerEventBus.RaiseHungerUpdated(currentValue, maxValue);
    }

    protected override void DecayStat()
    {
        base.DecayStat();
        PlayerEventBus.RaiseHungerUpdated(currentValue, maxValue);
    }
}

