using System;

public class GameEndEventBus
{
    public static event Action<GameOverReason> OnGameOver;
    public static void RaiseGameOver(GameOverReason reason) => OnGameOver?.Invoke(reason);

    public static event Action<string> OnTriggerGameOverUI;
    public static void RaiseTriggerGameOverUI(string message) => OnTriggerGameOverUI?.Invoke(message);
}
