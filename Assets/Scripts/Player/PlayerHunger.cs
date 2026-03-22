
public class PlayerHunger : PlayerStat
{

    void Update()
    {
        if (!active) return;

        if (Resource.IsEating)
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
        PlayerEventBus.RaiseHungerUpdated(currentValue, maxValue);
    }

    protected override void DecayStat()
    {
        base.DecayStat();
        PlayerEventBus.RaiseHungerUpdated(currentValue, maxValue);
    }
}

