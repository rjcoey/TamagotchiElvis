using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerStat : MonoBehaviour
{
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

    protected virtual void IncreaseStat()
    {
        currentValue = Mathf.Min(maxValue, currentValue + valueIncreaseRate * Time.deltaTime);

        if (!healthEventActive && currentValue > upperHealthEventThreshold * maxValue)
        {
            healthEffect.SetActive(true);
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

        if (currentValue < lowerHealthEventThreshold * maxValue)
        {
            healthEffect.SetActive(true);
            healthEventActive = true;
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
