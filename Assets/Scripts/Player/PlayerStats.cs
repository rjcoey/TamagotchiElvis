using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [field: SerializeField] public float MaxHunger { get; private set; } = 100.0f;
    [field: SerializeField] public float MaxHappiness { get; private set; } = 100.0f;
    [field: SerializeField] public float MaxTalent { get; private set; } = 100.0f;

    public float CurrentHunger { get; private set; }
    public float CurrentHappiness { get; private set; }
    public float CurrentTalent { get; private set; }

    [SerializeField] private float startHunger = 50.0f;
    [SerializeField] private float startHappiness = 50.0f;
    [SerializeField] private float startTalent = 50.0f;

    [SerializeField] private float hungerDecayRate = 1.0f;
    [SerializeField] private float happinessDecayRate = 1.0f;
    [SerializeField] private float talentDecayRate = 1.0f;

    public bool IsGameRunning { get; private set; } = false;

    void OnEnable()
    {
        ClockEventBus.OnStartDay += ResumeGame;
        PlayerEventBus.OnPauseGame += PauseGame;
    }

    void OnDisable()
    {
        ClockEventBus.OnStartDay -= ResumeGame;
        PlayerEventBus.OnPauseGame -= PauseGame;
    }

    void Start()
    {
        CurrentHunger = startHunger;
        CurrentHappiness = startHappiness;
        CurrentTalent = startTalent;

        PlayerEventBus.RaiseStartGame(this);
    }

    private void Update()
    {
        if (!IsGameRunning) return;

        if (!Resource.IsEating)
            CurrentHunger = LoseResource(CurrentHunger, hungerDecayRate * Time.deltaTime);

        if (!Resource.IsHappy)
            CurrentHappiness = LoseResource(CurrentHappiness, happinessDecayRate * Time.deltaTime);

        if (!Resource.IsPractising)
            CurrentTalent = LoseResource(CurrentTalent, talentDecayRate * Time.deltaTime);
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
        CurrentTalent = FillResource(CurrentTalent, fillRate * Time.deltaTime, MaxHappiness);
    }

    private float FillResource(float currentAmount, float amountToGain, float maxValue)
    {
        return Mathf.Min(currentAmount + amountToGain, maxValue);
    }

    private float LoseResource(float currentAmount, float amountToLose)
    {
        return Mathf.Max(currentAmount - amountToLose, 0.0f);
    }
}
