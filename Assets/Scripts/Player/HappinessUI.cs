using UnityEngine.UI;
using UnityEngine;

public class HappinessUI : StatUI
{
    void OnEnable()
    {
        PlayerEventBus.OnHappinessUpdated += UpdateUIBar;
    }

    void OnDisable()
    {
        PlayerEventBus.OnHappinessUpdated -= UpdateUIBar;
    }
}
