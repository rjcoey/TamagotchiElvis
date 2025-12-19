using System;

/// <summary>
/// A static event bus for handling game over and game end scenarios.
/// </summary>
public class GameEventBus
{
    public static event Action OnStartGame;
    public static void RaiseStartGame() => OnStartGame?.Invoke();

    public static event Action<GameOverReason> OnGameOver;
    public static void RaiseGameOver(GameOverReason reason) => OnGameOver?.Invoke(reason);

    public static event Action OnGameComplete;
    public static void RaiseGameComplete() => OnGameComplete?.Invoke();

    public static event Action<string> OnTriggerGameOverUI;
    public static void RaiseTriggerGameOverUI(string message) => OnTriggerGameOverUI?.Invoke(message);
}
