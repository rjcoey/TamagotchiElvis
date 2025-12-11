using UnityEngine;

public class DayTracker : MonoBehaviour
{
    [SerializeField] private float realTimeDuration = 300.0f;
    [SerializeField] private int dayDuration = 16;
    [SerializeField] private int dayStartTime = 8;

    private float realElapsedTime = 0.0f;

    private int gameElapsedMinutes = 0;
    private int gameElapsedHours = 0;
    private int currentGameHour = 0;
    private int lastUpdatedMinute = -1;

    private bool isTimerPaused = true;
    private string inGameTime;

    void OnEnable()
    {
        ClockEventBus.OnPauseTimer += PauseTimer;
        ClockEventBus.OnResumeTimer += ResumeTimer;
        ClockEventBus.OnStartDay += ResetTimer;
    }

    void OnDisable()
    {
        ClockEventBus.OnPauseTimer -= PauseTimer;
        ClockEventBus.OnResumeTimer -= ResumeTimer;
        ClockEventBus.OnStartDay -= ResetTimer;
    }

    void Update()
    {
        if (isTimerPaused) return;

        realElapsedTime += Time.deltaTime;

        float progress = Mathf.Clamp01(realElapsedTime / realTimeDuration);

        int totalGameMinutes = Mathf.FloorToInt(progress * dayDuration * 60.0f);
        totalGameMinutes = Mathf.Clamp(totalGameMinutes, 0, dayDuration * 60 - 1);

        gameElapsedHours = totalGameMinutes / 60;
        currentGameHour = dayStartTime + gameElapsedHours;
        gameElapsedMinutes = (totalGameMinutes % 60) + 1;

        if (gameElapsedMinutes != lastUpdatedMinute)
        {
            lastUpdatedMinute = gameElapsedMinutes;
            inGameTime = currentGameHour.ToString("00") + ":" + gameElapsedMinutes.ToString("00");
            ClockEventBus.RaiseTimeChanged(inGameTime);
        }

        if (realElapsedTime > realTimeDuration)
        {
            inGameTime = "00:00";

            ClockEventBus.RaiseTimeChanged(inGameTime);
            ClockEventBus.RaiseDayComplete();
        }
    }

    void ResumeTimer()
    {
        isTimerPaused = false;
    }

    void PauseTimer()
    {
        isTimerPaused = true;
    }

    void ResetTimer(int daysUntilGig)
    {
        realElapsedTime = 0.0f;
    }
}
