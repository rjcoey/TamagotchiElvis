using TMPro;
using UnityEngine;

public class CashUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;


    private void IncreaseCashUI(int cashTotal)
    {
        numberText.text = $"${cashTotal}";
    }

    private void DecreaseCashUI(int cashTotal)
    {
        numberText.text = $"${cashTotal}";
    }
}
