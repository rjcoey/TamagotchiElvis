public class Fridge : Resource
{
    public override void Use(PlayerStats playerStats)
    {
        Eat(playerStats);
    }

    public override void StopUsing()
    {
        StopEating();
    }

    public void Eat(PlayerStats stats)
    {
        if (!IsEating)
        {
            IsEating = true;
        }

        stats.FillHunger(resourceFillRate);
    }

    public void StopEating()
    {
        IsEating = false;
    }
}
