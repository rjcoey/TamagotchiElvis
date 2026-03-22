using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base class for all interactable resources in the game world (e.g., food, guitar, bed).
/// Provides common functionality for player interaction points and usage logic.
/// </summary>
public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceTooltipUI tooltip;
    [field: SerializeField] public Transform UsePoint { get; private set; }
    [SerializeField] private UpgradeLevel[] upgradeLevels = new UpgradeLevel[3];
    [SerializeField] private MeshRenderer[] meshRenderers;

    protected float resourceFillRate;
    public int CurrentLevel { get; private set; }
    public int UpgradeCost { get { return upgradeLevels[CurrentLevel].cost; } }

    public static bool IsEating = false;
    public static bool IsHappy = false;
    public static bool IsPractising = false;

    void Awake()
    {
        if (upgradeLevels.Length > 0)
        {
            resourceFillRate = upgradeLevels[0].fillRate;
        }
    }

    public virtual void Use() { }

    public virtual void StopUsing() { }

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

    public void TryUpgrade()
    {
        if (CurrentLevel >= upgradeLevels.Length - 1) return;
        // playerStats.DecreaseCash(UpgradeCost);
        CurrentLevel++;
        tooltip.InitTooltip(this);
    }
}

[System.Serializable]
public struct UpgradeLevel
{
    public int cost;
    public float fillRate;
}
