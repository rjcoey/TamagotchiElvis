using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceTooltipUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI levelText;

    private Resource currentResource;

    private RectTransform rectTransform;

    private InputAction pointAction;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        pointAction = InputSystem.actions.FindAction("Point");
    }

    void OnEnable()
    {
        UpdatePosition();
    }

    public void InitTooltip(Resource resource)
    {
        labelText.text = resource.name;
        upgradeText.text = $"Upgrade ${resource.UpgradeCost}";
        levelText.text = $"{resource.CurrentLevel + 1} / 3";
        currentResource = resource;
    }

    public void OnUseClicked()
    {
        PlayerEventBus.RaiseUseButtonClicked(currentResource);
    }

    public void OnUpgradeClicked()
    {
        currentResource.TryUpgrade();
    }

    void UpdatePosition()
    {
        Vector2 targetPosition = pointAction.ReadValue<Vector2>();


        float tooltipWidth = rectTransform.rect.width;
        float tooltipHeight = rectTransform.rect.height;

        float minX = 0.0f;
        float maxX = Screen.width - tooltipWidth;
        float minY = 0.0f;
        float maxY = Screen.height - tooltipHeight;

        float clampedX = Mathf.Clamp(targetPosition.x + 10f, minX, maxX);
        float clampedY = Mathf.Clamp(targetPosition.y + 10f, minY, maxY);

        rectTransform.position = new Vector2(clampedX, clampedY);
    }
}
