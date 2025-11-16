using TMPro;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private TextMeshProUGUI dayText;

    void OnEnable()
    {
        ClockEventBus.OnTimeChanged += UpdateClockText;
        ClockEventBus.OnNewDay += UpdateDayText;
    }

    void OnDisable()
    {
        ClockEventBus.OnTimeChanged -= UpdateClockText;
        ClockEventBus.OnNewDay -= UpdateDayText;
    }

    private void UpdateClockText(string newTime)
    {
        clockText.text = newTime;
    }

    private void UpdateDayText(int daysUntilGig)
    {
        dayText.text = daysUntilGig.ToString();
    }
}
