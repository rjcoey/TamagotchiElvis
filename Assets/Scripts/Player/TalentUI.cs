using UnityEngine;
using UnityEngine.UI;

public class TalentUI : MonoBehaviour
{
    [SerializeField] private Image talentFill;

    void OnEnable()
    {
        PlayerEventBus.OnTalentIncreased += IncreaseTalentUI;
        PlayerEventBus.OnTalentDecreased += DecreaseTalentUI;
    }

    void OnDisable()
    {
        PlayerEventBus.OnTalentIncreased -= IncreaseTalentUI;
        PlayerEventBus.OnTalentDecreased -= DecreaseTalentUI;
    }

    private void IncreaseTalentUI(float newFill, bool playFeedback)
    {
        talentFill.fillAmount = newFill;
    }

    private void DecreaseTalentUI(float newFill, bool playFeedback)
    {
        talentFill.fillAmount = newFill;
    }
}
