using TMPro;
using UnityEngine;

public class FansUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fansText;

    void OnEnable()
    {
        PlayerEventBus.OnFansUpdated += UpdateFansText;
    }

    void OnDisable()
    {
        PlayerEventBus.OnFansUpdated -= UpdateFansText;
    }

    void UpdateFansText(float fansTotal)
    {
        fansText.text = fansTotal.ToString("F0");
    }
}
