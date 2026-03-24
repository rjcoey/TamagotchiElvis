using System;
using UnityEngine;


public class PlayerEventBus
{
    public static event Action<float> OnFansUpdated;
    public static void RaiseUpdateFans(float fansTotal) => OnFansUpdated?.Invoke(fansTotal);
    public static event Action<float> OnCashUpdated;
    public static void RaiseUpdateCash(float cashTotal) => OnCashUpdated?.Invoke(cashTotal);

    public static event Action<float, float> OnHungerUpdated;
    public static void RaiseHungerUpdated(float currentHunger, float maxHunger) => OnHungerUpdated?.Invoke(currentHunger, maxHunger);

    public static event Action<float, float> OnHappinessUpdated;
    public static void RaiseHappinessUpdated(float currentHappiness, float maxHappiness) => OnHappinessUpdated?.Invoke(currentHappiness, maxHappiness);

    public static event Action<float, float> OnTalentUpdated;
    public static void RaiseTalentUpdated(float currentTalent, float maxTalent) => OnTalentUpdated?.Invoke(currentTalent, maxTalent);

    public static event Action<StatName, float> OnUseResource;
    public static void RaiseUseResource(StatName statName, float improveRate) => OnUseResource?.Invoke(statName, improveRate);

    public static event Action<StatName> OnStopUseResource;
    public static void RaiseStopUseResource(StatName statName) => OnStopUseResource?.Invoke(statName);

    public static event Action OnEnablePlayer;
    public static void RaiseEnablePlayer() => OnEnablePlayer?.Invoke();

    public static event Action OnDisablePlayer;
    public static void RaiseDisablePlayer() => OnDisablePlayer?.Invoke();

    public static event Action OnHideHUD;
    public static void RaiseHideHUD() => OnHideHUD?.Invoke();

    public static event Action OnShowHUD;
    public static void RaiseShowHUD() => OnShowHUD.Invoke();

    public static event Action<Resource> OnUseButtonClicked;
    public static void RaiseUseButtonClicked(Resource resource) => OnUseButtonClicked?.Invoke(resource);
}
