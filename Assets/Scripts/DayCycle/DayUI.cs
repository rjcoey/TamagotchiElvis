using System.Collections;
using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
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
        ClockEventBus.OnNewDay += StartDayUI;
    }

    void OnDisable()
    {
        ClockEventBus.OnNewDay -= StartDayUI;
    }

    private void StartDayUI(int dayNumber)
    {
        StartCoroutine(RunDayUI(dayNumber));
    }

    private IEnumerator RunDayUI(int daysUntilGig)
    {
        dayText.text = daysUntilGig.ToString();

        yield return canvasFader.FadeIn(fadeInDuration);
        yield return new WaitForSeconds(holdDuration);
        yield return canvasFader.FadeOut(fadeOutDuration);

        GameEventBus.RaiseResumeGame();
    }
}
