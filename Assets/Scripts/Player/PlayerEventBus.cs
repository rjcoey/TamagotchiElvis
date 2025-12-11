using System;


public class PlayerEventBus
{
    public static event Action<PlayerStats> OnSpawnPlayer;
    public static void RaiseSpawnPlayer(PlayerStats playerStats) => OnSpawnPlayer?.Invoke(playerStats);

    public static event Action<int> OnFansUpdated;
    public static void RaiseUpdateFans(int fansTotal) => OnFansUpdated?.Invoke(fansTotal);

    public static event Action OnEnablePlayer;
    public static void RaiseEnablePlayer() => OnEnablePlayer?.Invoke();

    public static event Action OnDisablePlayer;
    public static void RaiseDisablePlayer() => OnDisablePlayer?.Invoke();
}
