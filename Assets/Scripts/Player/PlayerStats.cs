using UnityEngine;

/// <summary>
/// Manages all core player statistics, including needs (hunger, happiness, talent),
/// resources (cash, fans), and their decay over time. Also responsible for checking
/// game over conditions related to these stats.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [Header("Stat Configuration")]
    [Tooltip("A curve that introduces a random performance factor for gigs.")]
    [field: SerializeField] public AnimationCurve PerformanceCurve = default;

    [field: SerializeField] public float MaxHunger { get; private set; } = 100.0f;
    [field: SerializeField] public float MaxHappiness { get; private set; } = 100.0f;
    [field: SerializeField] public float MaxTalent { get; private set; } = 100.0f;

    public float CurrentHunger { get; private set; }
    public float CurrentHappiness { get; private set; }
    public float CurrentTalent { get; private set; }


    public float GetHungerScore => CurrentHunger / MaxHunger;
    public float GetHappinessScore => CurrentHappiness / MaxHappiness;
    public float GetTalentScore => CurrentTalent / MaxTalent;

    public bool IsGameRunning { get; private set; } = false;

    [Header("Initial Values")]
    [SerializeField] private float startHunger = 50.0f;
    [SerializeField] private float startHappiness = 50.0f;
    [SerializeField] private float startTalent = 50.0f;

    [Header("Decay Rates")]
    [Tooltip("Amount of hunger lost per second when not eating.")]
    [SerializeField] private float hungerDecayRate = 1.0f;
    [SerializeField] private float happinessDecayRate = 1.0f;
    [SerializeField] private float talentDecayRate = 1.0f;

    private int currentFans = 0;
    // private int currentCash = 0;


    void OnEnable()
    {
        ClockEventBus.OnStartDay += ResumeGame;
        ClockEventBus.OnEndDay += PauseGame;
    }

    void OnDisable()
    {
        ClockEventBus.OnStartDay -= ResumeGame;
        ClockEventBus.OnEndDay -= PauseGame;
    }

    void Start()
    {
        CurrentHunger = startHunger;
        CurrentHappiness = startHappiness;
        CurrentTalent = startTalent;

        PlayerEventBus.RaiseStartGame(this);
        PlayerEventBus.RaiseUpdateFans(currentFans);
    }

    void Update()
    {
        if (!IsGameRunning) return;

        // --- Stat Decay Logic ---
        // If the player is not actively using a corresponding resource, decay the stat.
        if (!Resource.IsEating)
            CurrentHunger = LoseResource(CurrentHunger, hungerDecayRate * Time.deltaTime);

        if (!Resource.IsHappy)
            CurrentHappiness = LoseResource(CurrentHappiness, happinessDecayRate * Time.deltaTime);

        if (!Resource.IsPractising)
            CurrentTalent = LoseResource(CurrentTalent, talentDecayRate * Time.deltaTime);

        // --- Game Over Condition Checks ---
        // Check if any stat has hit a max or min threshold.
        if (CurrentHunger >= MaxHunger) TriggerGameOver(GameOverReason.Overeating);
        if (CurrentHunger <= 0.0f) TriggerGameOver(GameOverReason.Starvation);
        if (CurrentHappiness >= MaxHappiness) TriggerGameOver(GameOverReason.Retiring);
        if (CurrentHappiness <= 0.0f) TriggerGameOver(GameOverReason.Sadness);
        if (CurrentTalent >= MaxTalent) TriggerGameOver(GameOverReason.Burnout);
        if (CurrentTalent <= 0.0f) TriggerGameOver(GameOverReason.Sellout);
    }

    /// <summary>
    /// Triggers the game over sequence if the game is currently running.
    /// </summary>
    private void TriggerGameOver(GameOverReason reason)
    {
        if (!IsGameRunning) return;

        IsGameRunning = false;
        GameEndEventBus.RaiseGameOver(reason);
    }

    private void ResumeGame()
    {
        IsGameRunning = true;
    }

    private void PauseGame()
    {
        IsGameRunning = false;
    }

    public void FillHunger(float fillRate)
    {
        CurrentHunger = FillResource(CurrentHunger, fillRate * Time.deltaTime, MaxHunger);
    }

    public void FillHappiness(float fillRate)
    {
        CurrentHappiness = FillResource(CurrentHappiness, fillRate * Time.deltaTime, MaxHappiness);
    }

    public void FillTalent(float fillRate)
    {
        CurrentTalent = FillResource(CurrentTalent, fillRate * Time.deltaTime, MaxTalent);
    }

    private float FillResource(float currentAmount, float amountToGain, float maxValue)
    {
        return Mathf.Min(currentAmount + amountToGain, maxValue);
    }

    private float LoseResource(float currentAmount, float amountToLose)
    {
        return Mathf.Max(currentAmount - amountToLose, 0.0f);
    }

    public void AdjustFans(int fansAmount)
    {
        currentFans += fansAmount;
        PlayerEventBus.RaiseUpdateFans(currentFans);
    }
}
