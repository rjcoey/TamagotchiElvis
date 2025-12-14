using UnityEngine.UI;
using UnityEngine;
using System;

public class HappinessUI : MonoBehaviour
{
    [SerializeField] private Image happinessFill;

    void OnEnable()
    {
        PlayerEventBus.OnHappinessIncreased += IncreaseHappinessUI;
        PlayerEventBus.OnHappinessDecreased += DecreaseHappinessUI;
    }

    void OnDisable()
    {
        PlayerEventBus.OnHappinessIncreased -= IncreaseHappinessUI;
        PlayerEventBus.OnHappinessDecreased -= DecreaseHappinessUI;
    }

    private void IncreaseHappinessUI(float newFill, bool playFeedback)
    {
        happinessFill.fillAmount = newFill;
    }

    private void DecreaseHappinessUI(float newFill, bool playFeedback)
    {
        happinessFill.fillAmount = newFill;
    }
}
