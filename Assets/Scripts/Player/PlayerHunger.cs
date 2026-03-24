public class PlayerHunger : PlayerStat
{
    protected override void Start()
    {
        base.Start();
        PlayerEventBus.RaiseHungerUpdated(CurrentValue, maxValue);
    }

    protected override void Update()
    {
        if (!active) return;
        base.Update();

        if (isUsingResource)
        {
            FillStat(fillRate);
        }
        else
        {
            DecayStat();
        }
    }

    protected override void FillStat(float fillRate)
    {
        base.FillStat(fillRate);
        PlayerEventBus.RaiseHungerUpdated(CurrentValue, maxValue);
    }

    protected override void DecayStat()
    {
        base.DecayStat();
        PlayerEventBus.RaiseHungerUpdated(CurrentValue, maxValue);
    }
}

