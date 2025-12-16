using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceTooltipUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI levelText;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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
    }

    void UpdatePosition()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        float tooltipWidth = rectTransform.rect.width;
        float tooltipHeight = rectTransform.rect.height;

        float minX = 0.0f;
        float maxX = Screen.width - tooltipWidth;
        float minY = 0.0f;
        float maxY = Screen.height - tooltipHeight;

        float clampedX = Mathf.Clamp(mousePosition.x + 10f, minX, maxX);
        float clampedY = Mathf.Clamp(mousePosition.y + 10f, minY, maxY);

        rectTransform.position = new Vector2(clampedX, clampedY);
    }
}
