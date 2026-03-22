
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerHunger : PlayerStat
{
    [SerializeField] ScriptableRendererFeature hungerHealthEffect;

    protected override void Update()
    {
        if (!active) return;
        base.Update();

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

