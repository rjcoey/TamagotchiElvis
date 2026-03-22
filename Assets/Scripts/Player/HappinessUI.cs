using UnityEngine.UI;
using UnityEngine;

public class HappinessUI : MonoBehaviour
{
    [SerializeField] private Image happinessFill;

    void OnEnable()
    {
        PlayerEventBus.OnHappinessUpdated += UpdateHappinessUI;
    }

    void OnDisable()
    {
        PlayerEventBus.OnHappinessUpdated -= UpdateHappinessUI;
    }

    private void UpdateHappinessUI(float currentHappiness, float maxHappiness)
    {
        happinessFill.fillAmount = currentHappiness / maxHappiness;
    }
}
