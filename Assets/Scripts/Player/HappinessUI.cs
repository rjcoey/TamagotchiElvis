using UnityEngine.UI;
using UnityEngine;

public class HappinessUI : MonoBehaviour
{
    [SerializeField] private Image happinessFill;

    private PlayerStats playerStats;

    void OnEnable()
    {
        PlayerEventBus.OnGameStart += SetPlayerStats;
    }

    void OnDisable()
    {
        PlayerEventBus.OnGameStart -= SetPlayerStats;
    }

    private void SetPlayerStats(PlayerStats stats)
    {
        playerStats = stats;
    }
}
