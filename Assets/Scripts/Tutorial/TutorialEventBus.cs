using System;

public class TutorialEventBus
{
    public static event Action OnStartTutorial;
    public static void RaiseStartTutorial() => OnStartTutorial?.Invoke();

    public static event Action OnCompleteTutorial;
    public static void RaiseCompleteTutorial() => OnCompleteTutorial?.Invoke();
}
