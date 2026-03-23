using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerStat : MonoBehaviour
{
    [field: SerializeField] public StatName StatName { get; private set; }
    [SerializeField] private float startValue = 50.0f;
    [SerializeField] private float valueDecayRate = 0.5f;
    [SerializeField] private float valueIncreaseRate = 5.0f;
    [SerializeField] protected float maxValue = 100.0f;

    [SerializeField][Range(0, 1)] protected float lowerHealthEventThreshold = 0.1f;
    [SerializeField][Range(0, 1)] protected float upperHealthEventThreshold = 0.9f;

    [SerializeField] ScriptableRendererFeature healthEffect;

    [SerializeField] protected GameOverReason fullReason;
    [SerializeField] protected GameOverReason emptyReason;

    protected float currentValue;
    protected bool active = false;
    private bool healthEventActive = false;

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
        healthEffect.SetActive(false);
    }

    protected virtual void Update()
    {
        if (healthEventActive && currentValue > lowerHealthEventThreshold * maxValue && currentValue < upperHealthEventThreshold * maxValue)
        {
            healthEffect.SetActive(false);
            healthEventActive = false;
        }
    }

    public virtual void AdjustStat(float delta)
    {
        currentValue = Mathf.Clamp(currentValue + delta, 0.0f, maxValue);
        HandleHealthEvent();
        HandleGameOver();
    }

    protected virtual void ImproveStat()
    {
        currentValue = Mathf.Min(maxValue, currentValue + valueIncreaseRate * Time.deltaTime);
        HandleHealthEvent();
        HandleGameOver();
    }

    protected virtual void DecayStat()
    {
        currentValue = Mathf.Max(0.0f, currentValue - valueDecayRate * Time.deltaTime);
        HandleHealthEvent();
        HandleGameOver();
    }

    private void HandleHealthEvent()
    {
        if (!healthEventActive && currentValue > upperHealthEventThreshold * maxValue)
        {
            healthEffect.SetActive(true);
            healthEventActive = true;
        }
        else if (currentValue < lowerHealthEventThreshold * maxValue)
        {
            healthEffect.SetActive(true);
            healthEventActive = true;
        }
    }

    private void HandleGameOver()
    {
        if (currentValue >= maxValue)
        {
            GameManager.Instance.TriggerGameOver(fullReason);
        }
        else if (currentValue <= 0)
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
