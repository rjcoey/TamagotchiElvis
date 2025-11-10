using UnityEngine;
using UnityEngine.UI;

public class HungerUI : MonoBehaviour
{
    [SerializeField] private Image hungerFill;

    private PlayerStats playerStats;

    void OnEnable()
    {
        PlayerEventBus.OnGameStart += SetPlayerStats;
    }

    private void OnDisable()
    {
        PlayerEventBus.OnGameStart -= SetPlayerStats;
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
