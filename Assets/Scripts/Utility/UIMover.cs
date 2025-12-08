using System.Collections;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    [SerializeField] private AnimationCurve revealCurve;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public IEnumerator LerpPanelPosition(Vector2 startPosition, Vector2 endPosition, float duration = 0.5f)
    {
        float timeElapsed = 0f;

        rectTransform.anchoredPosition = startPosition;

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
