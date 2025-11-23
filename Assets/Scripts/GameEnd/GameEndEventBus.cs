using System;

/// <summary>
/// A static event bus for handling game over and game end scenarios.
/// </summary>
public class GameEndEventBus
{
    /// <summary>
    /// Event fired when the game's state officially transitions to "Game Over".
    /// This should be raised by the central game manager when a loss condition is met.
    /// </summary>
    /// <param name="reason">The specific reason why the game ended.</param>
    public static event Action<GameOverReason> OnGameOver;

    /// <summary>
    /// Raises the OnGameOver event to signal that the game has ended.
    /// </summary>
    /// <param name="reason">The reason for the game over.</param>
    public static void RaiseGameOver(GameOverReason reason) => OnGameOver?.Invoke(reason);

    /// <summary>
    /// Event fired to trigger the display of the Game Over UI.
    /// This is separate from OnGameOver to allow for delays or animations before showing the UI.
    /// </summary>
    /// <param name="message">The message to be displayed on the game over screen (e.g., "You ran out of money!").</param>
    public static event Action<string> OnTriggerGameOverUI;

    /// <summary>
    /// Raises the OnTriggerGameOverUI event to show the game over screen.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void RaiseTriggerGameOverUI(string message) => OnTriggerGameOverUI?.Invoke(message);
}
