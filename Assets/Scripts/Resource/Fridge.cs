public class Fridge : Resource
{
    public override void Use()
    {
        Eat();
    }

    public override void StopUsing()
    {
        StopEating();
    }

    private void Eat()
    {
        if (!IsEating)
        {
            IsEating = true;
        }
    }

    public void StopEating()
    {
        IsEating = false;
    }
}
