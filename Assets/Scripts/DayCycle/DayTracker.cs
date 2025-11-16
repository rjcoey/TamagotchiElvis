using UnityEngine;

public class DayTracker : MonoBehaviour
{
    [SerializeField] private float realTimeDuration = 300.0f;
    [SerializeField] private int dayDuration = 16;
    [SerializeField] private int dayStartTime = 8;

    private GigDataSO currentGigData;

    private int daysUntilGig = 0;
    private float realElapsedTime = 0.0f;

    private int gameElapsedMinutes = 0;
    private int gameElapsedHours = 0;
    private int currentGameHour = 0;
    private int lastUpdatedMinute = -1;

    private bool dayComplete = true;
    private string inGameTime;

    void OnEnable()
    {
        GigEventBus.OnGigSelected += SetCurrentGig;
        ClockEventBus.OnStartDay += StartDay;
    }

    void OnDisable()
    {
        GigEventBus.OnGigSelected -= SetCurrentGig;
        ClockEventBus.OnStartDay -= StartDay;
    }

    void Update()
    {
        if (dayComplete) return;

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
            dayComplete = true;
            inGameTime = "00:00";
            ClockEventBus.RaiseTimeChanged(inGameTime);
            daysUntilGig--;

            PlayerEventBus.RaisePauseGame();
            if (daysUntilGig == 0)
            {
                Debug.Log("Play gig here");
            }
            else
            {
                ClockEventBus.RaiseNewDay(daysUntilGig);
            }
        }
    }

    void SetCurrentGig(GigDataSO gigData)
    {
        currentGigData = gigData;
        daysUntilGig = currentGigData.DaysUntilGig;

        ClockEventBus.RaiseNewDay(daysUntilGig);
    }

    void StartDay()
    {
        dayComplete = false;
        realElapsedTime = 0.0f;
    }
}
