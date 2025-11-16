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

    public void StartFadeIn(float fadeTime = 2.0f)
    {
        StartCoroutine(Fade(0, 1, fadeTime));
    }

    public void StartFadeOut(float fadeTime = 2.0f)
    {
        StartCoroutine(Fade(1, 0, fadeTime));
    }

    public IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float timeElapsed = 0f;
        canvasGroup.interactable = false;

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
