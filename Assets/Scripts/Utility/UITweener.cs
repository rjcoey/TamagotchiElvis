using System.Collections;
using UnityEngine;

public static class UITweener
{
    public static IEnumerator LerpElementSize(Transform element, Vector3 startSize, Vector3 endSize, float duration, AnimationCurve scaleCurve)
    {
        element.localScale = startSize;
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            element.localScale = Vector3.LerpUnclamped(startSize, endSize, scaleCurve.Evaluate(t));
            yield return null;
        }

        element.localScale = endSize;
    }
}
