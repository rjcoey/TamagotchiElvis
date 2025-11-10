using System;
using UnityEngine;

public class PlayerEventBus
{
    public static event Action<PlayerStats> OnGameStart;

    public static void RaiseGameStart(PlayerStats playerStats) => OnGameStart?.Invoke(playerStats);
}
