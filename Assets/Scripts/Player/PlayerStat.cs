using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private float startValue = 50.0f;
    [SerializeField] private float valueDecayRate = 0.5f;
    [SerializeField] private float valueIncreaseRate = 5.0f;
    [SerializeField] protected float maxValue = 100.0f;
    [SerializeField][Range(0, 1)] protected float upperHealthEventThreshold;

    [SerializeField] protected GameOverReason fullReason;
    [SerializeField] protected GameOverReason emptyReason;

    protected float currentValue;
    protected bool active = false;
    private static bool healthEventActive = false;

    void OnEnable()
    {
        PlayerEventBus.OnEnablePlayer += ActivateStat;
        PlayerEventBus.OnDisablePlayer += DisableStat;
    }

    void OnDisable()
    {
        PlayerEventBus.OnEnablePlayer -= ActivateStat;
        PlayerEventBus.OnDisablePlayer -= DisableStat;
    }

    void Start()
    {
        ResetStat();
    }

    private void ResetStat()
    {
        currentValue = startValue;
    }

    protected virtual void Update()
    {
        if (healthEventActive && currentValue > 0.1f * maxValue && currentValue < 0.9f * maxValue)
        {
            PlayerEventBus.RaiseEndHealthEvent();
            healthEventActive = false;
        }
    }

    protected virtual void IncreaseStat()
    {
        currentValue = Mathf.Min(maxValue, currentValue + valueIncreaseRate * Time.deltaTime);

        if (!healthEventActive && currentValue > 0.9f * maxValue)
        {
            PlayerEventBus.RaiseStartHealthEvent();
            healthEventActive = true;
        }

        if (currentValue >= maxValue)
        {
            GameManager.Instance.TriggerGameOver(fullReason);
        }
    }

    protected virtual void DecayStat()
    {
        currentValue = Mathf.Max(0.0f, currentValue - valueDecayRate * Time.deltaTime);

        if (currentValue < 0.1f * maxValue)
        {
            PlayerEventBus.RaiseStartHealthEvent();
        }

        if (currentValue <= 0)
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
