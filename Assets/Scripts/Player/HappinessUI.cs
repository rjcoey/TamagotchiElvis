using UnityEngine.UI;
using UnityEngine;

public class HappinessUI : MonoBehaviour
{
    [SerializeField] private Image happinessFill;

    private PlayerStats playerStats;

    void OnEnable()
    {
        PlayerEventBus.OnSpawnPlayer += SetPlayerStats;
    }

    void OnDisable()
    {
        PlayerEventBus.OnSpawnPlayer -= SetPlayerStats;
    }

    void Update()
    {
        if (playerStats == null) return;
        happinessFill.fillAmount = playerStats.CurrentHappiness / playerStats.MaxHappiness;
    }

    private void SetPlayerStats(PlayerStats stats)
    {
        playerStats = stats;
    }
}
