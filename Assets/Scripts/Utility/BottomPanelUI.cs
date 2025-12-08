using UnityEngine;

[RequireComponent(typeof(UIMover))]
public class BottomPanelUI : MonoBehaviour
{
    [SerializeField] private float revealTime = 0.5f;

    private RectTransform rectTransform;
    private UIMover hudPanel;

    void OnEnable()
    {
        ClockEventBus.OnStartDay += RevealBottomPanel;
        ClockEventBus.OnEndDay += HideBottomPanel;
    }

    void OnDisable()
    {
        ClockEventBus.OnStartDay -= RevealBottomPanel;
        ClockEventBus.OnEndDay -= HideBottomPanel;
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        hudPanel = GetComponent<UIMover>();
    }

    private void RevealBottomPanel()
    {
        Vector2 startPosition = new(rectTransform.anchoredPosition.x, -rectTransform.sizeDelta.y);
        Vector2 endPosition = new(rectTransform.anchoredPosition.x, 0.0f);
        StartCoroutine(hudPanel.LerpPanelPosition(startPosition, endPosition, revealTime));
    }

    private void HideBottomPanel()
    {
        Vector2 endPosition = new(rectTransform.anchoredPosition.x, -rectTransform.sizeDelta.y);
        StartCoroutine(hudPanel.LerpPanelPosition(rectTransform.anchoredPosition, endPosition, revealTime));
    }
}
