using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [field: SerializeField] public float MaxHunger { get; private set; } = 100.0f;
    [field: SerializeField] public float MaxHappiness { get; private set; } = 100.0f;
    [field: SerializeField] public float MaxTalent { get; private set; } = 100.0f;

    [SerializeField] private float hungerDecayRate = 1.0f;
    [SerializeField] private float happinessDecayRate = 1.0f;
    [SerializeField] private float talentDecayRate = 1.0f;

    public float CurrentHunger { get; private set; }
    public float CurrentHappiness { get; private set; }
    public float CurrentTalent { get; private set; }

    public bool IsEating { get; private set; } = false;
    public bool IsEntertained { get; private set; } = false;

    void Start()
    {
        CurrentHunger = MaxHunger;
        CurrentHappiness = MaxHappiness;
        CurrentTalent = MaxTalent;

        PlayerEventBus.RaiseGameStart(this);
    }

    private void Update()
    {
        if (!IsEating)
            CurrentHunger = LoseResource(CurrentHunger, hungerDecayRate * Time.deltaTime);

        if (!IsEntertained)
            CurrentHappiness = LoseResource(CurrentHappiness, happinessDecayRate * Time.deltaTime);
    }

    public void Eat(float hungerGainRate)
    {
        CurrentHunger = FillResource(CurrentHunger, hungerGainRate * Time.deltaTime, MaxHunger);
        IsEating = true;
    }

    public void StopEating()
    {
        IsEating = false;
    }

    public void WatchTV(float happinessGainRate)
    {
        CurrentHappiness = FillResource(CurrentHappiness, happinessGainRate * Time.deltaTime, MaxHappiness);
        IsEntertained = true;
    }

    public void StopWatchingTV()
    {
        IsEntertained = false;
    }

    public float FillResource(float currentAmount, float amountToGain, float maxValue)
    {
        return Mathf.Min(currentAmount + amountToGain, maxValue);
    }

    private float LoseResource(float currentAmount, float amountToLose)
    {
        return Mathf.Max(currentAmount - amountToLose, 0.0f);
    }
}
