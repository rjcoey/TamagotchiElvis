using System.Collections;
using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    [SerializeField] private GameFlagsSO gameFlags;

    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float holdDuration = 3.0f;
    [SerializeField] private float fadeOutDuration = 1.5f;

    CanvasFader canvasFader;

    void Awake()
    {
        canvasFader = GetComponent<CanvasFader>();
    }

    void OnEnable()
    {
        ClockEventBus.OnStartDay += StartDayUI;
    }

    void OnDisable()
    {
        ClockEventBus.OnStartDay -= StartDayUI;
    }

    private void StartDayUI(int daysUntilGig)
    {
        StartCoroutine(RunDayUI(daysUntilGig));
    }

    private IEnumerator RunDayUI(int daysUntilGig)
    {
        dayText.text = daysUntilGig.ToString();

        yield return canvasFader.Co_FadeIn(fadeInDuration);
        yield return new WaitForSeconds(holdDuration);
        yield return canvasFader.Co_FadeOut(fadeOutDuration);

        ClockEventBus.RaiseResumeTimer();
        PlayerEventBus.RaiseEnablePlayer();
    }
}
