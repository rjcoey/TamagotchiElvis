using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    private PlayerStat[] playerStats;

    void Awake()
    {
        playerStats = GetComponents<PlayerStat>();
    }

    public void AdjustStat(StatName statName, float delta)
    {
        foreach (PlayerStat stat in playerStats)
        {
            if (stat.StatName == statName)
            {
                stat.AdjustStat(delta);
            }
        }
    }
}
