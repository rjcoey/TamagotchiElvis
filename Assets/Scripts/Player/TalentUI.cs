using UnityEngine;
using UnityEngine.UI;

public class TalentUI : StatUI
{
    void OnEnable()
    {
        PlayerEventBus.OnTalentUpdated += UpdateUIBar;
    }

    void OnDisable()
    {
        PlayerEventBus.OnTalentUpdated -= UpdateUIBar;
    }
}
