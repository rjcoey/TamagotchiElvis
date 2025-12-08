using UnityEngine;
using UnityEngine.UI;

public class HungerUI : MonoBehaviour
{
    [SerializeField] private Image hungerFill;

    private PlayerStats playerStats;

    void OnEnable()
    {
        PlayerEventBus.OnSpawnPlayer += SetPlayerStats;
    }

    private void OnDisable()
    {
        PlayerEventBus.OnSpawnPlayer -= SetPlayerStats;
    }

    void Update()
    {
        if (playerStats == null) return;

        hungerFill.fillAmount = playerStats.CurrentHunger / playerStats.MaxHunger;
    }

    private void SetPlayerStats(PlayerStats stats)
    {
        playerStats = stats;
    }
}
