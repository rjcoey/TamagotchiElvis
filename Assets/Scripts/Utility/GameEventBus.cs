using System;
using UnityEngine;

public class GameEventBus
{
    public static event Action OnPauseGame;
    public static void RaisePauseGame() => OnPauseGame?.Invoke();

    public static event Action OnResumeGame;
    public static void RaiseResumeGame() => OnResumeGame?.Invoke();
}
