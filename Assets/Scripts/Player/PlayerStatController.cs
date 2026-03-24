using System;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    private PlayerStat[] playerStats;

    void OnEnable()
    {
        PlayerEventBus.OnUseResource += UseResource;
        PlayerEventBus.OnStopUseResource += StopUseResource;
    }

    void OnDisable()
    {
        PlayerEventBus.OnUseResource -= UseResource;
        PlayerEventBus.OnStopUseResource -= StopUseResource;
    }

    void Awake()
    {
        playerStats = GetComponents<PlayerStat>();
    }

    public float GetStatScore(StatName statName)
    {
        PlayerStat stat = GetStatByName(statName);
        return stat.GetScore();
    }

    public void AdjustStat(StatName statName, float delta)
    {
        PlayerStat stat = GetStatByName(statName);

        stat.AdjustStat(delta);
    }

    public bool CanUpgrade(float upgradeCost)
    {
        return GetStatByName(StatName.CASH).CurrentValue >= upgradeCost;
    }

    public PlayerStat GetStatByName(StatName statName)
    {
        foreach (PlayerStat stat in playerStats)
        {
            if (stat.StatName == statName)
            {
                return stat;
            }
        }
        return null;
    }

    private void UseResource(StatName statName, float fillRate)
    {
        PlayerStat stat = GetStatByName(statName);
        stat.StartStatFill(fillRate);
    }

    private void StopUseResource(StatName statName)
    {
        PlayerStat stat = GetStatByName(statName);
        stat.StopStatFill();
    }
}
