using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages the entire UI sequence for playing a gig, from the intro animation to displaying the results.
/// </summary>
public class GigPlayer : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform reviewLabel;
    [SerializeField] private TextMeshProUGUI starsText;
    [SerializeField] private TextMeshProUGUI reviewText;
    [SerializeField] private TextMeshProUGUI cashText;
    [SerializeField] private TextMeshProUGUI fansText;
    [SerializeField] private TextMeshProUGUI instructionText;

    [Header("Animation Curves")]
    [Tooltip("Curve for the bouncy scale animation of UI elements.")]
    [SerializeField] private AnimationCurve scaleCurve;
    [Tooltip("Curve for the fade-in/out animation of the instruction text.")]
    [SerializeField] private AnimationCurve fadeCurve;

    [Header("Animation Timings")]
    [SerializeField] private float typewriterSpeed = 0.05f;

    private CanvasFader canvasFader;
    private PlayerStats playerStats;
    private InputAction clickAction;

    void OnEnable()
    {
        PlayerEventBus.OnSpawnPlayer += SetPlayerStats;
        GigEventBus.OnPlayGig += StartRunGig;
    }

    void OnDisable()
    {
        // Unsubscribe from events to prevent memory leaks and errors.
        PlayerEventBus.OnSpawnPlayer -= SetPlayerStats; // Corrected from += to -=
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

    /// <summary>
    /// Entry point to start the gig sequence. Resets all UI elements and starts the main coroutine.
    /// </summary>
    void StartRunGig(GigDataSO gigData)
    {
        // Reset UI to a clean state before starting the sequence.
        titleText.text = string.Empty;
        reviewLabel.localScale = Vector3.zero;
        starsText.text = string.Empty;
        reviewText.text = string.Empty;
        cashText.text = string.Empty;
        fansText.text = string.Empty;
        instructionText.alpha = 0.0f;

        StartCoroutine(RunGig(gigData));
    }

    /// <summary>
    /// Calculates the player's gig score based on their stats and an element of randomness.
    /// </summary>
    /// <returns>A final score between 0.0 and 1.0.</returns>
    private float CalculateGigScore(GigDataSO gigData)
    {
        // Get player stats (normalized between 0 and 1).
        float hunger = playerStats.GetHungerScore;
        float happiness = playerStats.GetHappinessScore;
        float talent = playerStats.GetTalentScore;
        // Add a random performance factor based on a curve.
        float performance = playerStats.PerformanceCurve.Evaluate(Random.value);

        // Average the stats to get a raw performance value.
        float rawGigScore = (hunger + happiness + talent + performance) / 4.0f;
        // Apply the gig's specific difficulty curve to get the final score.
        float finalScore = gigData.GigDifficultyCurve.Evaluate(rawGigScore);
        return finalScore;
    }

    /// <summary>
    /// The main coroutine that controls the entire gig animation sequence step-by-step.
    /// </summary>
    private IEnumerator RunGig(GigDataSO gigData)
    {
        // --- Intro ---
        yield return canvasFader.Co_FadeIn();
        yield return Typewriter.TypewriterEffect(titleText, gigData.GigLocation, typewriterSpeed);

        // --- Review ---
        yield return LerpElementSize(reviewLabel, Vector3.zero, Vector3.one, 0.5f);

        float gigScore = CalculateGigScore(gigData);
        int numberOfStars = Mathf.RoundToInt(gigScore * 5.0f);
        string starRating = string.Concat(Enumerable.Repeat("*", numberOfStars));
        yield return Typewriter.TypewriterEffect(starsText, starRating, typewriterSpeed);

        // Determine which review text to show based on the score.
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
        yield return Typewriter.TypewriterEffect(reviewText, review, typewriterSpeed);

        // --- Rewards ---
        int cash = Mathf.RoundToInt(gigData.BaseGigCash * gigScore);
        yield return CountToNumber(cashText, cash, 1.0f, "$");
        int fans = Mathf.RoundToInt(gigData.BaseGigFans * gigScore);
        yield return CountToNumber(fansText, fans, 1.0f, "+");

        // Apply the rewards to the player's stats.
        playerStats.AdjustFans(fans);

        // --- Wait for Continue ---
        yield return FadeInstruction(0, 1, 0.2f);
        yield return new WaitUntil(() => clickAction.WasPerformedThisFrame());

        // --- Outro ---
        yield return canvasFader.Co_FadeOut();
        GigEventBus.RaiseGigComplete();
    }

    #region --- Animation Coroutines ---

    /// <summary>
    /// Animates the scale of a Transform using a curve for a bouncy effect.
    /// </summary>
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

    /// <summary>
    /// Animates a TMP text element to count up from 0 to a target value.
    /// </summary>
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

    /// <summary>
    /// Fades the alpha of the instruction text using a curve.
    /// </summary>
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
    #endregion
}
