using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GigPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform reviewLabel;
    [SerializeField] private TextMeshProUGUI starsText;
    [SerializeField] private TextMeshProUGUI reviewText;
    [SerializeField] private TextMeshProUGUI cashText;
    [SerializeField] private TextMeshProUGUI fansText;
    [SerializeField] private TextMeshProUGUI instructionText;

    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private AnimationCurve fadeCurve;

    [SerializeField] private float typewriterSpeed = 0.05f;

    private CanvasFader canvasFader;
    private PlayerStats playerStats;

    private InputAction clickAction;

    void OnEnable()
    {
        PlayerEventBus.OnStartGame += SetPlayerStats;
        GigEventBus.OnPlayGig += StartRunGig;
    }

    void OnDisable()
    {
        PlayerEventBus.OnStartGame += SetPlayerStats;
        GigEventBus.OnPlayGig -= StartRunGig;
    }

    void Awake()
    {
        canvasFader = GetComponent<CanvasFader>();
        clickAction = InputSystem.actions.FindAction("Click");
    }

    void SetPlayerStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }

    void StartRunGig(GigDataSO gigData)
    {
        titleText.text = string.Empty;
        reviewLabel.localScale = Vector3.zero;
        starsText.text = string.Empty;
        reviewText.text = string.Empty;
        cashText.text = string.Empty;
        fansText.text = string.Empty;
        instructionText.alpha = 0.0f;

        StartCoroutine(RunGig(gigData));
    }

    private float CalculateGigScore(GigDataSO gigData)
    {
        float hunger = playerStats.GetHungerScore;
        float happiness = playerStats.GetHappinessScore;
        float talent = playerStats.GetTalentScore;
        float performance = playerStats.PerformanceCurve.Evaluate(Random.value);

        float rawGigScore = (hunger + happiness + talent + performance) / 4.0f;
        float finalScore = gigData.GigDifficultyCurve.Evaluate(rawGigScore);
        return finalScore;
    }

    private IEnumerator RunGig(GigDataSO gigData)
    {
        yield return canvasFader.Fade(0, 1, 0.5f);

        yield return TypewriterEffect(titleText, gigData.GigLocation);

        yield return LerpElementSize(reviewLabel, Vector3.zero, Vector3.one, 0.5f);

        float gigScore = CalculateGigScore(gigData);
        Debug.Log(gigScore);
        int numberOfStars = Mathf.RoundToInt(gigScore * 5.0f);
        string starRating = string.Concat(Enumerable.Repeat("*", numberOfStars));
        yield return TypewriterEffect(starsText, starRating);

        string review;
        if (gigScore > 0.7f)
        {
            review = gigData.PositiveReview;
        }
        else if (gigScore < 0.4f)
        {
            review = gigData.NegativeReview;
        }
        else
        {
            review = gigData.NeutralReview;
        }
        yield return TypewriterEffect(reviewText, review);

        int cash = Mathf.RoundToInt(gigData.BaseGigCash * gigScore);
        yield return CountToNumber(cashText, cash, 1.0f, "$");
        int fans = Mathf.RoundToInt(gigData.BaseGigFans * gigScore);
        yield return CountToNumber(fansText, fans, 1.0f, "+");

        playerStats.AdjustFans(fans);

        yield return FadeInstruction(0, 1, 0.2f);

        yield return new WaitUntil(() => clickAction.WasPerformedThisFrame());

        yield return canvasFader.Fade(1.0f, 0.0f, 0.5f);

        GigEventBus.RaiseGigComplete();
    }

    private IEnumerator TypewriterEffect(TextMeshProUGUI textElement, string textToType)
    {
        textElement.text = string.Empty;
        foreach (char c in textToType)
        {
            textElement.text += c;
            yield return new WaitForSeconds(typewriterSpeed);
        }
        textElement.text = textToType;
    }

    private IEnumerator LerpElementSize(Transform element, Vector3 startSize, Vector3 endSize, float duration)
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

    private IEnumerator CountToNumber(TextMeshProUGUI textElement, int targetValue, float duration, string prefix = "")
    {
        float timeElapsed = 0.0f;
        int startValue = 0;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            int currentValue = (int)Mathf.Lerp(startValue, targetValue, t);
            textElement.text = prefix + currentValue.ToString();
            yield return null;
        }

        textElement.text = prefix + targetValue.ToString();
    }

    private IEnumerator FadeInstruction(float startAlpha, float endAlpha, float duration)
    {
        instructionText.alpha = startAlpha;
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            instructionText.alpha = Mathf.Lerp(startAlpha, endAlpha, fadeCurve.Evaluate(t));
            yield return null;
        }

        instructionText.alpha = endAlpha;
    }
}
