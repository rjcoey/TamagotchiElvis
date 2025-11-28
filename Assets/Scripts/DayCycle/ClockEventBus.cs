using System;

public class ClockEventBus
{
    public static event Action<string> OnTimeChanged;
    public static void RaiseTimeChanged(string timeString) => OnTimeChanged?.Invoke(timeString);

    public static event Action<int> OnDayAdvanced;

    /// <summary>
    /// This is called to trigger the Day UI
    /// </summary>
    public static void RaiseDayAdvanced(int daysUntilGig) => OnDayAdvanced?.Invoke(daysUntilGig);

    public static event Action OnStartDay;
    /// <summary>
    /// This is called when the Day UI finished displaying the day and the clock timer starts
    /// </summary>
    public static void RaiseStartDay() => OnStartDay?.Invoke();

    public static event Action OnEndDay;
    public static void RaiseEndDay() => OnEndDay?.Invoke();
}

