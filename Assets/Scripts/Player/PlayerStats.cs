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

    void Start()
    {
        CurrentHunger = MaxHunger;
        CurrentHappiness = MaxHappiness;
        CurrentTalent = MaxTalent;

        PlayerEventBus.RaiseGameStart(this);
    }

    private void Update()
    {
        CurrentHunger = Mathf.Max(CurrentHunger - hungerDecayRate * Time.deltaTime, 0.0f);
    }
}
