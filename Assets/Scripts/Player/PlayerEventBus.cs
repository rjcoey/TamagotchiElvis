using System;
using UnityEngine;

public class PlayerEventBus
{
    public static event Action<PlayerStats> OnStartGame;
    public static void RaiseStartGame(PlayerStats playerStats) => OnStartGame?.Invoke(playerStats);

    public static event Action OnPauseGame;
    public static void RaisePauseGame() => OnPauseGame?.Invoke();

    public static event Action OnResumeGame;
    public static void RaiseResumeGame() => OnResumeGame?.Invoke();
}
