using UnityEngine;
using UnityEngine.UI;

public class TalentUI : MonoBehaviour
{
    [SerializeField] private Image talentFill;


    void OnEnable()
    {
        PlayerEventBus.OnTalentUpdated += UpdateTalentUI;
    }

    void OnDisable()
    {
        PlayerEventBus.OnTalentUpdated -= UpdateTalentUI;
    }

    private void UpdateTalentUI(float currentTalent, float maxTalent)
    {
        talentFill.fillAmount = currentTalent / maxTalent;
    }
}
