using System.Collections;
using UnityEngine;

public class CanvasFader : MonoBehaviour
{
    [SerializeField] private AnimationCurve fadeCurve;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetAlphaImmediate(float value)
    {
        canvasGroup.alpha = value;
    }

    public IEnumerator FadeIn(float fadeDuration = 2.0f)
    {
        yield return Fade(0.0f, 1.0f, fadeDuration);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public IEnumerator FadeOut(float fadeDuration = 2.0f)
    {
        yield return Fade(1, 0, fadeDuration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            float t = timeElapsed / duration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, fadeCurve.Evaluate(t));
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
