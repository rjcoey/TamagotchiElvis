using System;

public class PlayerEventBus
{
    public static event Action<PlayerStats> OnStartGame;
    public static void RaiseStartGame(PlayerStats playerStats) => OnStartGame?.Invoke(playerStats);

    public static event Action<int> OnFansUpdated;
    public static void RaiseUpdateFans(int fansTotal) => OnFansUpdated?.Invoke(fansTotal);

}
