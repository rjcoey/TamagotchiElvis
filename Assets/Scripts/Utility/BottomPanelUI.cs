using System.Collections;
using UnityEngine;

public class BottomPanelUI : MonoBehaviour
{
    [SerializeField] private AnimationCurve revealCurve;
    [SerializeField] private float revealTime = 0.5f;

    private RectTransform rectTransform;

    void OnEnable()
    {
        PlayerEventBus.OnResumeGame += RevealBottomPanel;
        PlayerEventBus.OnPauseGame += HideBottomPanel;
    }

    void OnDisable()
    {
        PlayerEventBus.OnResumeGame -= RevealBottomPanel;
        PlayerEventBus.OnPauseGame -= HideBottomPanel;
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void RevealBottomPanel()
    {
        Vector2 startPosition = new(rectTransform.anchoredPosition.x, -rectTransform.sizeDelta.y);
        Vector2 endPosition = new(rectTransform.anchoredPosition.x, 0.0f);
        StartCoroutine(LerpPanelPosition(startPosition, endPosition, revealTime));
    }

    private void HideBottomPanel()
    {
        Vector2 endPosition = new(rectTransform.anchoredPosition.x, -rectTransform.sizeDelta.y);
        StartCoroutine(LerpPanelPosition(rectTransform.anchoredPosition, endPosition, revealTime));
    }

    private IEnumerator LerpPanelPosition(Vector2 startPosition, Vector2 endPosition, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            float t = timeElapsed / duration;
            rectTransform.anchoredPosition = Vector2.LerpUnclamped(startPosition, endPosition, revealCurve.Evaluate(t));
            yield return null;
        }

        rectTransform.anchoredPosition = endPosition;
    }
}
