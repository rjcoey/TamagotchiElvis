using UnityEngine;

public class Guitar : Resource
{
    public override void Use()
    {
        Practise();
    }

    public override void StopUsing()
    {
        StopPractising();
    }

    private void Practise()
    {
        if (!IsPractising)
        {
            IsPractising = true;
        }
    }

    private void StopPractising()
    {
        IsPractising = false;
    }
}
