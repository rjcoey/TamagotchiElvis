using System;

/// <summary>
/// A static event bus for events related to the player's core state and progression.
/// </summary>
public class PlayerEventBus
{
    /// <summary>
    /// Event fired once at the very beginning of the game, after the PlayerStats object is initialized.
    /// Provides a central reference to the PlayerStats component for any system that needs it.
    /// </summary>
    /// <param name="playerStats">A reference to the singleton PlayerStats instance.</param>
    public static event Action<PlayerStats> OnSpawnPlayer;
    public static void RaiseSpawnPlayer(PlayerStats playerStats) => OnSpawnPlayer?.Invoke(playerStats);

    /// <summary>
    /// Event fired whenever the player's total number of fans changes.
    /// UI elements can listen to this to update their display.
    /// </summary>
    /// <param name="fansTotal">The new total number of fans.</param>
    public static event Action<int> OnFansUpdated;
    public static void RaiseUpdateFans(int fansTotal) => OnFansUpdated?.Invoke(fansTotal);
}
