public class PlayerFans : PlayerStat
{
    protected override void Start()
    {
        base.Start();
        PlayerEventBus.RaiseUpdateFans(CurrentValue);
    }

    public override void AdjustStat(float delta)
    {
        base.AdjustStat(delta);
        PlayerEventBus.RaiseUpdateFans(CurrentValue);
    }
}
