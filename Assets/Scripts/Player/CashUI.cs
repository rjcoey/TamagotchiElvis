using System;
using TMPro;
using UnityEngine;

public class CashUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;

    void OnEnable()
    {
        PlayerEventBus.OnCashUpdated += UpdateCash;
    }

    void OnDisable()
    {
        PlayerEventBus.OnCashUpdated -= UpdateCash;
    }

    private void UpdateCash(float cashTotal)
    {
        numberText.text = $"${cashTotal:F0}";
    }
}
