using UnityEngine;
using UnityEngine.UI;

public class HungerUI : MonoBehaviour
{
    [SerializeField] private Image hungerFill;

    void OnEnable()
    {
        PlayerEventBus.OnHungerUpdated += UpdateHungerUI;
    }

    void OnDisable()
    {
        PlayerEventBus.OnHungerUpdated -= UpdateHungerUI;
    }

    private void UpdateHungerUI(float currentHunger, float maxHunger)
    {
        hungerFill.fillAmount = currentHunger / maxHunger;
    }
}
