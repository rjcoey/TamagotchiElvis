using System;

public class TutorialEventBus
{
    public static bool HasPlayedTutorial = false;

    public static event Action OnStartTutorial;
    public static void RaiseStartTutorial() => OnStartTutorial?.Invoke();
}
