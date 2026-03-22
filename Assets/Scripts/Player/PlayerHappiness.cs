
public class PlayerHappiness : PlayerStat
{
    void Update()
    {
        if (!active) return;

        if (Resource.IsHappy)
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
        PlayerEventBus.RaiseHappinessUpdated(currentValue, maxValue);
    }

    protected override void DecayStat()
    {
        base.DecayStat();
        PlayerEventBus.RaiseHappinessUpdated(currentValue, maxValue);
    }
}
