using System;

public class ClockEventBus
{
    public static event Action<string> OnTimeChanged;
    public static void RaiseTimeChanged(string timeString) => OnTimeChanged?.Invoke(timeString);

    public static event Action<int> OnStartDay;
    public static void RaiseStartDay(int daysUntilGig) => OnStartDay?.Invoke(daysUntilGig);

    public static event Action OnDayComplete;
    public static void RaiseDayComplete() => OnDayComplete?.Invoke();

    public static event Action OnPauseTimer;
    public static void RaisePauseTimer() => OnPauseTimer?.Invoke();

    public static event Action OnResumeTimer;
    public static void RaiseResumeTimer() => OnResumeTimer?.Invoke();
}

