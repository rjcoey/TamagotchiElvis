using UnityEngine;

/// <summary>
/// Base class for all interactable resources in the game world (e.g., food, guitar, bed).
/// Provides common functionality for player interaction points and usage logic.
/// </summary>
public class Resource : MonoBehaviour
{
    [SerializeField] private StatName statName;
    [SerializeField] private ResourceTooltipUI tooltip;
    [field: SerializeField] public Transform UsePoint { get; private set; }
    [SerializeField] private UpgradeLevel[] upgradeLevels = new UpgradeLevel[3];
    [SerializeField] private MeshRenderer[] meshRenderers;

    protected float fillRate;
    public int CurrentLevel { get; private set; }
    public int UpgradeCost { get { return upgradeLevels[CurrentLevel].cost; } }

    private PlayerStatController playerStats;

    void Awake()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStatController>();

        if (upgradeLevels.Length > 0)
        {
            fillRate = upgradeLevels[0].fillRate;
        }
    }

    public void Use()
    {
        PlayerEventBus.RaiseUseResource(statName, fillRate);
    }

    public void StopUsing()
    {
        PlayerEventBus.RaiseStopUseResource(statName);
    }

    public void ShowToolTip()
    {
        tooltip.InitTooltip(this);
        tooltip.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }

    public void HighlightResource()
    {
        foreach (MeshRenderer mesh in meshRenderers)
        {
            foreach (Material material in mesh.materials)
            {
                material.EnableKeyword("_FLASH");
            }
        }
    }

    public void EndHighlight()
    {
        foreach (MeshRenderer mesh in meshRenderers)
        {
            foreach (Material material in mesh.materials)
            {
                material.DisableKeyword("_FLASH");
            }
        }
    }

    public bool TryUpgrade()
    {
        // Cancel upgrade and return false if resource already maxxed out
        if (CurrentLevel >= upgradeLevels.Length - 1) return false;

        // Cancel upgrade and return false if not enough cash 
        if (!playerStats.CanUpgrade(UpgradeCost)) return false;

        // Run upgrade
        playerStats.AdjustStat(StatName.CASH, -UpgradeCost);
        CurrentLevel++;
        fillRate = upgradeLevels[CurrentLevel].fillRate;
        tooltip.InitTooltip(this);
        return true;
    }
}

[System.Serializable]
public struct UpgradeLevel
{
    public int cost;
    public float fillRate;
}
