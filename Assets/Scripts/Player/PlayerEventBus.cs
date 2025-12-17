using System;
using UnityEngine;


public class PlayerEventBus
{
    public static event Action<PlayerStats> OnSpawnPlayer;
    public static void RaiseSpawnPlayer(PlayerStats playerStats) => OnSpawnPlayer?.Invoke(playerStats);

    public static event Action<int> OnFansUpdated;
    public static void RaiseUpdateFans(int fansTotal) => OnFansUpdated?.Invoke(fansTotal);

    public static event Action<float, bool> OnHungerIncreased;
    public static void RaiseHungerIncreased(float newHunger, bool playFeedback) => OnHungerIncreased?.Invoke(newHunger, playFeedback);

    public static event Action<float, bool> OnHungerDecreased;
    public static void RaiseHungerDecreased(float newHunger, bool playFeedback) => OnHungerDecreased?.Invoke(newHunger, playFeedback);

    public static event Action<float, bool> OnHappinessIncreased;
    public static void RaiseHappinessIncreased(float newHappiness, bool playFeedback) => OnHappinessIncreased?.Invoke(newHappiness, playFeedback);

    public static event Action<float, bool> OnHappinessDecreased;
    public static void RaiseHappinessDecreased(float newHappiness, bool playFeedback) => OnHappinessDecreased?.Invoke(newHappiness, playFeedback);

    public static event Action<float, bool> OnTalentIncreased;
    public static void RaiseTalentIncreased(float newTalent, bool playFeedback) => OnTalentIncreased?.Invoke(newTalent, playFeedback);

    public static event Action<float, bool> OnTalentDecreased;
    public static void RaiseTalentDecreased(float newTalent, bool playFeedback) => OnTalentDecreased?.Invoke(newTalent, playFeedback);

    public static event Action<int> OnCashIncreased;
    public static void RaiseCashIncreased(int cashTotal) => OnCashIncreased?.Invoke(cashTotal);
    public static event Action<int> OnCashDecreased;
    public static void RaiseCashDecreased(int cashTotal) => OnCashDecreased?.Invoke(cashTotal);

    public static event Action OnEnablePlayer;
    public static void RaiseEnablePlayer() => OnEnablePlayer?.Invoke();

    public static event Action OnDisablePlayer;
    public static void RaiseDisablePlayer() => OnDisablePlayer?.Invoke();

    public static event Action<Resource> OnUseButtonClicked;
    public static void RaiseUseButtonClicked(Resource resource) => OnUseButtonClicked?.Invoke(resource);
}
