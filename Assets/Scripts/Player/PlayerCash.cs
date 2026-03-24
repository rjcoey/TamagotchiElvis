public class PlayerCash : PlayerStat
{
    protected override void Start()
    {
        base.Start();
        PlayerEventBus.RaiseUpdateCash(CurrentValue);
    }

    public override void AdjustStat(float delta)
    {
        base.AdjustStat(delta);
        PlayerEventBus.RaiseUpdateCash(CurrentValue);
    }
}
