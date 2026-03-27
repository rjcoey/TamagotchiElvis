
public class PlayerTalent : PlayerStat
{
    protected override void Start()
    {
        base.Start();
        PlayerEventBus.RaiseTalentUpdated(CurrentValue, maxValue);
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

    public override void AdjustStat(float delta)
    {
        base.AdjustStat(delta);
        PlayerEventBus.RaiseTalentUpdated(CurrentValue, maxValue);
    }

    protected override void FillStat(float fillRate)
    {
        base.FillStat(fillRate);
        PlayerEventBus.RaiseTalentUpdated(CurrentValue, maxValue);
    }

    protected override void DecayStat()
    {
        base.DecayStat();
        PlayerEventBus.RaiseTalentUpdated(CurrentValue, maxValue);
    }
}
