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
    private int currentCash = 0;


    void OnEnable()
    {
        PlayerEventBus.OnEnablePlayer += ResumeGame;
        PlayerEventBus.OnDisablePlayer += PauseGame;
    }

    void OnDisable()
    {
        PlayerEventBus.OnEnablePlayer -= ResumeGame;
        PlayerEventBus.OnDisablePlayer -= PauseGame;
    }

    void Start()
    {
        CurrentHunger = startHunger;
        CurrentHappiness = startHappiness;
        CurrentTalent = startTalent;

        PlayerEventBus.RaiseSpawnPlayer(this);
        PlayerEventBus.RaiseUpdateFans(currentFans);
    }

    void Update()
    {
        if (!IsGameRunning) return;

        // Check if any stat has hit a max or min threshold.
        if (CurrentHunger >= MaxHunger) TriggerGameOver(GameOverReason.Overeating);
        if (CurrentHunger <= 0.0f) TriggerGameOver(GameOverReason.Starvation);
        if (CurrentHappiness >= MaxHappiness) TriggerGameOver(GameOverReason.Retiring);
        if (CurrentHappiness <= 0.0f) TriggerGameOver(GameOverReason.Sadness);
        if (CurrentTalent >= MaxTalent) TriggerGameOver(GameOverReason.Burnout);
        if (CurrentTalent <= 0.0f) TriggerGameOver(GameOverReason.Sellout);

        // --- Stat Decay Logic ---
        // If the player is not actively using a corresponding resource, decay the stat.
        if (!Resource.IsEating)
            DecreaseHunger(hungerDecayRate * Time.deltaTime);

        if (!Resource.IsHappy)
            DecreaseHappiness(happinessDecayRate * Time.deltaTime);

        if (!Resource.IsPractising)
            DecreaseTalent(talentDecayRate * Time.deltaTime);
    }

    /// <summary>
    /// Triggers the game over sequence if the game is currently running.
    /// </summary>
    private void TriggerGameOver(GameOverReason reason)
    {
        if (!IsGameRunning) return;

        IsGameRunning = false;
        ClockEventBus.RaisePauseTimer();
        GameEventBus.RaiseGameOver(reason);
    }

    private void ResumeGame()
    {
        IsGameRunning = true;
    }

    private void PauseGame()
    {
        IsGameRunning = false;
    }

    public void IncreaseStatImmediate(STAT stat, float amount)
    {
        switch (stat)
        {
            case STAT.NULL:
                break;
            case STAT.HUNGER:
                IncreaseHunger(amount, true);
                break;
            case STAT.HAPPINESS:
                IncreaseHappiness(amount, true);
                break;
            case STAT.TALENT:
                IncreaseTalent(amount, true);
                break;
            case STAT.FANS:
                AdjustFans(Mathf.RoundToInt(amount));
                break;
            case STAT.CASH:
                break;
            default:
                break;
        }
    }

    public void DecreaseStatImmediate(STAT stat, float amount)
    {
        switch (stat)
        {
            case STAT.NULL:
                break;
            case STAT.HUNGER:
                DecreaseHunger(amount, true);
                break;
            case STAT.HAPPINESS:
                DecreaseHappiness(amount, true);
                break;
            case STAT.TALENT:
                DecreaseTalent(amount, true);
                break;
            case STAT.FANS:
                AdjustFans(-Mathf.RoundToInt(amount));
                break;
            case STAT.CASH:
                break;
            default:
                break;
        }

    }

    public void IncreaseHunger(float amount, bool playFeedback = false)
    {
        CurrentHunger = Mathf.Min(MaxHunger, CurrentHunger + amount);
        if (CurrentHunger >= MaxHunger)
        {
            TriggerGameOver(GameOverReason.Overeating);
        }
        PlayerEventBus.RaiseHungerIncreased(CurrentHunger / MaxHunger, playFeedback);
    }

    private void DecreaseHunger(float amount, bool playFeedback = false)
    {
        CurrentHunger = Mathf.Max(0.0f, CurrentHunger - amount);
        if (CurrentHunger <= 0)
        {
            TriggerGameOver(GameOverReason.Starvation);
        }
        PlayerEventBus.RaiseHungerDecreased(CurrentHunger / MaxHunger, playFeedback);
    }

    public void IncreaseHappiness(float amount, bool playFeedback = false)
    {
        CurrentHappiness = Mathf.Min(MaxHappiness, CurrentHappiness + amount);
        if (CurrentHappiness >= MaxHappiness)
        {
            TriggerGameOver(GameOverReason.Retiring);
        }
        PlayerEventBus.RaiseHappinessIncreased(CurrentHappiness / MaxHappiness, playFeedback);
    }

    private void DecreaseHappiness(float amount, bool playFeedback = false)
    {
        CurrentHappiness = Mathf.Max(0.0f, CurrentHappiness - amount);
        if (CurrentHappiness <= 0)
        {
            TriggerGameOver(GameOverReason.Sadness);
        }
        PlayerEventBus.RaiseHappinessDecreased(CurrentHappiness / MaxHappiness, playFeedback);
    }

    public void IncreaseTalent(float amount, bool playFeedback = false)
    {
        CurrentTalent = Mathf.Min(MaxTalent, CurrentTalent + amount);
        if (CurrentTalent >= MaxTalent)
        {
            TriggerGameOver(GameOverReason.Burnout);
        }
        PlayerEventBus.RaiseTalentIncreased(CurrentTalent / MaxTalent, playFeedback);
    }

    private void DecreaseTalent(float amount, bool playFeedback = false)
    {
        CurrentTalent = Mathf.Max(0.0f, CurrentTalent - amount);
        if (CurrentTalent <= 0)
        {
            TriggerGameOver(GameOverReason.Sellout);
        }
        PlayerEventBus.RaiseTalentDecreased(CurrentTalent / MaxTalent, playFeedback);
    }

    public void IncreaseCash(int amount)
    {
        currentCash += amount;
        PlayerEventBus.RaiseCashIncreased(currentCash);
    }

    public void DecreaseCash(int amount)
    {
        currentCash -= amount;
        PlayerEventBus.RaiseCashDecreased(currentCash);
    }

    public void AdjustFans(int fansAmount)
    {
        currentFans += fansAmount;
        PlayerEventBus.RaiseUpdateFans(currentFans);
    }
}
