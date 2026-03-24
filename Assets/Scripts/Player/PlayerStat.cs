using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerStat : MonoBehaviour
{
    [field: SerializeField] public StatName StatName { get; private set; }
    [SerializeField] private float startValue = 50.0f;
    [SerializeField] private float valueDecayRate = 0.5f;
    [SerializeField] protected float maxValue = 100.0f;

    [SerializeField][Range(0, 1)] protected float lowerHealthEventThreshold = 0.1f;
    [SerializeField][Range(0, 1)] protected float upperHealthEventThreshold = 0.9f;

    [SerializeField] ScriptableRendererFeature healthEffect;

    [SerializeField] protected GameOverReason fullReason;
    [SerializeField] protected GameOverReason emptyReason;

    [SerializeField] private bool healthStat = true;

    public float CurrentValue { get; private set; }
    protected float fillRate = 0.0f;
    protected bool active = false;
    protected bool isUsingResource = false;

    private bool healthEventActive = false;


    protected virtual void OnEnable()
    {
        PlayerEventBus.OnEnablePlayer += ActivateStat;
        PlayerEventBus.OnDisablePlayer += DisableStat;
    }

    protected virtual void OnDisable()
    {
        PlayerEventBus.OnEnablePlayer -= ActivateStat;
        PlayerEventBus.OnDisablePlayer -= DisableStat;
    }

    protected virtual void Start()
    {
        ResetStat();
    }

    protected virtual void Update()
    {
        if (healthEventActive && CurrentValue > lowerHealthEventThreshold * maxValue && CurrentValue < upperHealthEventThreshold * maxValue)
        {
            healthEffect.SetActive(false);
            healthEventActive = false;
        }
    }

    private void ResetStat()
    {
        CurrentValue = startValue;
        healthEffect?.SetActive(false);
    }

    public virtual void StartStatFill(float fillRate)
    {
        isUsingResource = true;
        this.fillRate = fillRate;
    }

    public void StopStatFill()
    {
        isUsingResource = false;
    }

    public float GetScore()
    {
        return CurrentValue / maxValue;
    }

    public virtual void AdjustStat(float delta)
    {
        CurrentValue = Mathf.Clamp(CurrentValue + delta, 0.0f, maxValue);
        HandleHealthEvent();
        HandleGameOver();
    }

    protected virtual void FillStat(float improveRate)
    {
        CurrentValue = Mathf.Min(maxValue, CurrentValue + improveRate * Time.deltaTime);
        HandleHealthEvent();
        HandleGameOver();
    }

    protected virtual void DecayStat()
    {
        CurrentValue = Mathf.Max(0.0f, CurrentValue - valueDecayRate * Time.deltaTime);
        HandleHealthEvent();
        HandleGameOver();
    }

    private void HandleHealthEvent()
    {
        if (!healthStat) return;

        if (!healthEventActive && CurrentValue > upperHealthEventThreshold * maxValue)
        {
            healthEffect.SetActive(true);
            healthEventActive = true;
        }
        else if (CurrentValue < lowerHealthEventThreshold * maxValue)
        {
            healthEffect.SetActive(true);
            healthEventActive = true;
        }
    }

    private void HandleGameOver()
    {
        if (!healthStat) return;

        if (CurrentValue >= maxValue)
        {
            GameManager.Instance.TriggerGameOver(fullReason);
        }
        else if (CurrentValue <= 0)
        {
            GameManager.Instance.TriggerGameOver(emptyReason);
        }
    }

    private void ActivateStat()
    {
        active = true;
    }

    private void DisableStat()
    {
        active = false;
    }
}
