using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField] private Image barFill;

    [SerializeField] private Color baseColor;
    [SerializeField] private Color goodFlashColor;
    [SerializeField] private Color badFlashColor;
    [SerializeField] private float flashDuration = 0.5f;
    [SerializeField] private Material fillMaterial;
    [SerializeField] private AnimationCurve upFlashCurve;
    [SerializeField] private AnimationCurve downFlashCurve;

    [SerializeField][Range(0, 1)] private float feedbackThreshold = 0.05f;

    private float previousFill = 0.5f;

    void Start()
    {
        fillMaterial.SetFloat("_Flash_Intensity", 0.0f);
    }

    protected void UpdateUIBar(float currentHunger, float maxHunger)
    {
        // if over a certain negative amount, flash red 
        // if over a certain positive amount, flash white 
        float newFill = currentHunger / maxHunger;
        float difference = newFill - previousFill;

        if (Mathf.Abs(difference) >= feedbackThreshold)
        {
            if (difference > 0)
                StartCoroutine(Co_FlashBar(goodFlashColor));
            if (difference < 0)
                StartCoroutine(Co_FlashBar(badFlashColor));
        }

        previousFill = newFill;
        barFill.fillAmount = currentHunger / maxHunger;
    }

    private IEnumerator Co_FlashBar(Color flashColor)
    {
        fillMaterial.SetColor("_Flash_Color", flashColor);
        fillMaterial.SetFloat("_Flash_Intensity", 0.0f);

        float timeElapsed = 0.0f;

        while (timeElapsed < flashDuration)
        {
            float t = Mathf.Lerp(0.0f, 1.0f, timeElapsed / flashDuration);
            t = upFlashCurve.Evaluate(t);
            fillMaterial.SetFloat("_Flash_Intensity", t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fillMaterial.SetFloat("_Flash_Intensity", 1.0f);
        timeElapsed = 0.0f;
        fillMaterial.SetColor("_Flash_Color", baseColor);

        while (timeElapsed < flashDuration)
        {
            float t = Mathf.Lerp(1.0f, 0.0f, timeElapsed / flashDuration);
            t = downFlashCurve.Evaluate(t);
            fillMaterial.SetFloat("_Flash_Intensity", t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        fillMaterial.SetFloat("_Flash_Intensity", 0.0f);
    }
}

