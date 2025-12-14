using TMPro;
using UnityEngine;

public class CashUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;

    void OnEnable()
    {
        PlayerEventBus.OnCashIncreased += IncreaseCashUI;
        PlayerEventBus.OnCashDecreased += DecreaseCashUI;
    }

    void OnDisable()
    {
        PlayerEventBus.OnCashIncreased -= IncreaseCashUI;
        PlayerEventBus.OnCashDecreased -= DecreaseCashUI;
    }

    private void IncreaseCashUI(int cashTotal)
    {
        numberText.text = $"${cashTotal}";
    }

    private void DecreaseCashUI(int cashTotal)
    {
        numberText.text = $"${cashTotal}";
    }
}
