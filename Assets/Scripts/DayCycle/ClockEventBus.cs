using System;

public class ClockEventBus
{
    public static event Action<string> OnTimeChanged;
    public static void RaiseTimeChanged(string timeString) => OnTimeChanged?.Invoke(timeString);

    public static event Action<int> OnNewDay;
    public static void RaiseNewDay(int daysUntilGig) => OnNewDay?.Invoke(daysUntilGig);

    public static event Action OnStartDay;
    public static void RaiseStartDay() => OnStartDay?.Invoke();

    public static event Action OnEndDay;
    public static void RaiseEndDay() => OnEndDay?.Invoke();

    public static event Action OnPauseTimer;
    public static void RaisePauseTimer() => OnPauseTimer?.Invoke();

    public static event Action OnResumeTimer;
    public static void RaiseResumeTimer() => OnResumeTimer?.Invoke();
}

