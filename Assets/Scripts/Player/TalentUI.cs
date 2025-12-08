using UnityEngine;
using UnityEngine.UI;

public class TalentUI : MonoBehaviour
{
    [SerializeField] private Image talentFill;

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
        talentFill.fillAmount = playerStats.CurrentTalent / playerStats.MaxTalent;
    }

    private void SetPlayerStats(PlayerStats stats)
    {
        playerStats = stats;
    }
}
