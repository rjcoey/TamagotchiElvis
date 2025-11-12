public class Fridge : Resource
{
    public override void Use(PlayerStats playerStats)
    {
        playerStats.Eat(resourceFillRate);
    }

    public override void StopUsing(PlayerStats playerStats)
    {
        playerStats.StopEating();
    }
}
